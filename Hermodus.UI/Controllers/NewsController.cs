﻿
using Hermodus.Data;
using Hermodus.Service;
using Hermodus.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using X.PagedList;

namespace Hermodus.UI.Controllers
{
    public class NewsController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly ISettingRepository repositorySetting;

        public NewsController(IPostRepository PostRepo,ISettingRepository repoSetting)
        {
               postRepository=PostRepo;
            repositorySetting = repoSetting;
        }
        // GET: Home
        public ActionResult Index(int? page)
        {
            Setting _NewsPageSetting;
            _NewsPageSetting = repositorySetting.GetSetting;
            //Setting to Show and Hiden Widgets
            //string _DisplayLastCategory;
            //if (_HomePageSetting.DisplayLastCategory == false)
            //     { _DisplayLastCategory = "none"; }
            //else { _DisplayLastCategory = "block"; }
            //string _DisplayLastPost;
            //if (_HomePageSetting.DisplayLastPost== false)
            //     { _DisplayLastPost = "none"; }
            //else { _DisplayLastPost = "block"; }
            //string _GoogleAdvWidget;
            //if (_HomePageSetting.DisplayGoogleWidget == false)
            //     { _GoogleAdvWidget = "none"; }
            //else { _GoogleAdvWidget = "block"; }

            int PageSize = _NewsPageSetting.PostNumberInPage;
            PostViewModel model = new PostViewModel
            {
                Posts = postRepository.PostList
                .OrderBy(p => p.PostId)
                .OrderByDescending(p => p.Create_time)
                .ToPagedList(page ?? 1, PageSize),



          
                //DisplayLastCategory = _DisplayLastCategory,
                //DisplayLastPost = _DisplayLastPost,
                //DisplayGoogleAdv= _GoogleAdvWidget

            };
            return View(model);
        }

        
        public ActionResult Feed()
        {
            IEnumerable<Post> posts = postRepository.PostIEnum.OrderByDescending(c => c.Create_time).Take(10);
            string websiteRoot = Request.Url.GetLeftPart(UriPartial.Authority);//Full Website address with protocol
           
            var feed = new SyndicationFeed("Alaeddin Blog", "Hermodus RSS Feed",
                    new Uri(websiteRoot+"//home//feed"),
                    Guid.NewGuid().ToString(),
                    DateTime.Now);

            var items = new List<SyndicationItem>();

            //var d = Request.Url.OriginalString; ////@localhost:58025/home/feed
            //var x = Request.Url.PathAndQuery; ///home/feed
            //var dd = Request.Url.PathAndQuery;//home/feed
            //var dddd = Request.Url.LocalPath;
            //var ddd = Request.Url.AbsolutePath;
            //var ddwsd = Request.Url.AbsoluteUri;
           







            foreach (Post _post in posts)
            {
                string posturl = String.Format(@"/Post/Details/{0}", _post.PostId);
                
                string postlink = websiteRoot + posturl;
                var item = new SyndicationItem(_post.Title , _post.Post_Content,new Uri(postlink));
                items.Add(item);
            }
            feed.Items = items;
            return new RssActionResult { Feed = feed };
        }
    }
}