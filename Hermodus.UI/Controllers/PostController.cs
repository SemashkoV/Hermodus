﻿using Hermodus.Data;
using Hermodus.Service;
using Hermodus.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace Hermodus.UI.Controllers
{
    
    public class PostController : Controller
    {
        private readonly IPostRepository repositoryPost;
        private readonly ICategoryRepository repositoryCategory;
        private readonly ICommentRepository repositoryComment;
        private readonly ISettingRepository repositorySetting;
        // GET: Post
        public PostController(IPostRepository repoPost,ICategoryRepository repoCategory,
                                                       ICommentRepository repoComment,
                                                       ISettingRepository repoSetting)
        {
            repositoryPost = repoPost;
            repositoryCategory = repoCategory;
            repositoryComment = repoComment;
            repositorySetting = repoSetting;
        }
        [Authorize(Roles = "Admin")]
        //public ActionResult Index(int page = 1)
        //{
        //    int PageSize = 5;
        //    PostViewModel model = new PostViewModel
        //    {
        //        Posts = repositoryPost.PostIEnum
        //        .OrderBy(p => p.PostId)
        //        .OrderByDescending(p => p.Create_time)
        //        .Skip((page - 1) * PageSize)
        //        .Take(PageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = PageSize,
        //            TotalItems = repositoryPost.PostList.Count()
        //        },
        //    };
        //    return View(model);
        //}

       
        public ActionResult Index()
        {
            //  List<Post> model = repositoryPost.PostList.OrderBy(p => p.PostId).Skip(1 - 1 * 5).Take(5).ToList();
            IEnumerable<Post> model = repositoryPost.PostList.OrderBy(p => p.PostId)
            .OrderByDescending(p => p.Create_time);
             
            return View(model);
        }
        //[AllowAnonymous]
        //public ActionResult Index1(int Id, int? page)
        //{
        //    IEnumerable<Post> model;//Get aproved Comment for Post 
        //   model = repositoryPost.PostList.Where(p => p.PostId == Id && p.Publish == true)
        ////        .OrderByDescending(p => p.Create_time).Distinct().ToList().ToPagedList(page ?? 1, 3);

        ////    List<Post> model = repositoryPost.PostList.OrderBy(p => p.PostId).Skip(1 - 1 * 5).Take(5).ToList();
        ////    IEnumerable<Post> model = repositoryPost.PostList.OrderBy(p => p.PostId)
        ////    .OrderByDescending(p => p.Create_time)
        ////    .Skip((page - 1) * 20)
        ////    .Take(20).ToList();
        //    return View(model);
        //}
     
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult PostByCategory(int? category, int? page)
        {
            PostViewModel model = new PostViewModel
            {
                Posts = repositoryPost.PostList
                .Where(p => p.CategoryId == category)
                .OrderBy(p => p.PostId)
                .OrderByDescending(p => p.Create_time)
                .ToPagedList(page ?? 1, 5)
                ,
                CurrentCategory = category
            };
           
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult NewPost()
        {
            Post model = GetPostSession();
            model.PostId = 0;//Becouse HttpPost NewPost, Need Id to know Edit or AddNew .
            model.CategoryDetails = repositoryCategory.CategoryIEnum;

            return View(model);
           
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> NewPost(Post data)
        {
            Post obj = GetPostSession();
           
            if (ModelState.IsValid)
            {

                var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
                int _CurrentUserId = Convert.ToInt32(identity.User.UserId);
                if (_CurrentUserId == 0)
                {
                    //becouse Sometime id = 0 ?????!!!! maybe session die???????
                    return View(data);
                }
              
                obj.PostId = data.PostId;
                obj.Title = data.Title;
                obj.Post_Content = data.Post_Content;

               
                obj.Update_time = DateTime.Now;//Need solution for this field no need any value.
                obj.UserId = _CurrentUserId;
                obj.Tages = data.Tages;
                obj.CategoryId = data.CategoryId;
                obj.FeaturedImage = data.FeaturedImage;
                obj.Frequence = 0;//If not 0 will be Null on DB , we cant  do Null +1 .
                if (data.PostId == 0)
                {
                    obj.Create_time = DateTime.Now;
                    repositoryCategory.IncreaseFreqOne(data.CategoryId);//How many time we use this category
                }
                else
                {
                    obj.Create_time = data.Create_time;
                }
               await repositoryPost.SaveAsync(obj);
                int? Newid = obj.PostId;
                if (obj != null)
                {
                    if(data.PostId == 0)
                    { 
                      TempData["message"] = string.Format("{0} добавлено успешно", obj.Title);
                     }
                    else
                    {
                      TempData["message"] = string.Format("{0} изменено успешно", obj.Title);
                    }
                }
                return RedirectToAction("Details", new { Id = Newid });
            }
            data.CategoryDetails = repositoryCategory.CategoryIEnum;
            return View(data);
        }
        [AllowAnonymous]
        //To Get Post By Id with Comments related
        public ActionResult Details(int? Id,int? page)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostViewModel model = new PostViewModel
            {
                Post = repositoryPost.Details(Id),
                Comments= repositoryComment.CommentList.Where(p => p.PostId == Id && p.Publish == true)
                .OrderByDescending(p => p.Create_time).ToPagedList(page ?? 1, 5)//5 Is Comment number in one page
        };
             


          // model.UserDetails.FName
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int ?Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post model = repositoryPost.Details(Id);
            model.CategoryDetails = repositoryCategory.CategoryIEnum;

            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewPost.chtml to save copy same page 
            return View("NewPost",model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Post post = repositoryPost.Details(Id);
           
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int? Id)
        {
            Post post = repositoryPost.Details(Id);
            if (post.Frequence != 0)
            {
                string CommnetError = string.Format("Вы не можете удалить этот пост, он имеет {0} комментариев", post.Frequence.ToString());
                //  ModelState.AddModelError("", CommnetError);
                TempData["message"] = CommnetError;
                return View(post);
            }
            Post _post = repositoryPost.Delete(Id);
            repositoryCategory.DecreaseFreqOne(post.CategoryId);//How many time we use this category
            if (_post != null)
            {
                TempData["message"] = string.Format("{0} удалено", post.Title);
            }
            return RedirectToAction("Index","Post");
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult LastPost()
        {
            Setting _NumOfLastPost;
            _NumOfLastPost = repositorySetting.GetSetting;
            IEnumerable<Post> Model;
            Model = repositoryPost.PostList.OrderByDescending(c => c.Create_time).Take(_NumOfLastPost.NumberOfLastPost);
            return PartialView("_LastPost",Model);
        }
        public ActionResult TopPost()
        {
            Setting _NumOfTopPost;
            _NumOfTopPost = repositorySetting.GetSetting;

            IEnumerable<Post> Model;
            Model = repositoryPost.PostList.OrderByDescending(p =>p.Frequence).Take(_NumOfTopPost.NumberOfTopPost);
            return PartialView("_TopPost", Model);
        }
        
        public ActionResult Tag(string teg)
        {
            IEnumerable<Post> Model;
            Model = repositoryPost.PostList.Where(p => p.Tages.Contains(teg));
            return View(Model);
        }

        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult PostByTag(string tag, int? page)
        {

            PostViewModel model = new PostViewModel
            {
                Posts = repositoryPost.PostList
               .Where(p => p.Tages.StartsWith(tag) || p.Tages.EndsWith(tag))
               .OrderBy(p => p.PostId)
               .OrderByDescending(p => p.Create_time)
               .ToPagedList(page ?? 1, 5)
               ,
                CurrentTag = tag
            };

            return View(model); 
        }
        ///Sessions
        ///

        private Post GetPostSession()
        {
            if (Session["post"] == null)
            {
                Session["post"] = new Post();
            }
            return (Post)Session["post"];
        }

    }
}