﻿using Hermodus.Data;
using Hermodus.Service;
using Hermodus.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hermodus.UI.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageRepository repositoryPage;
        //  private readonly IUserRepository repositoryUser;
   


        public PageController(IPageRepository repoPage)
        {
            repositoryPage = repoPage;
            //repositoryUser = repoUser;
        }
        // GET: Page
        public ActionResult Index()
        {


            IEnumerable<Page> model = repositoryPage.PageList;
              // ,UserDetails = repositoryUser.UserIEmum
            
            return View(model);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page model = repositoryPage.Details(Id);

            // model.UserDetails.FName
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddNewPage()
        {
            Page model = GetPageSession();
            model.PageId = 0;//Becouse HttpPost NewPost, Need Id to know Edit or AddNew .
           
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddNewPage(Page data)
        {
            if (ModelState.IsValid)
            {

                var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
                int _CurrentUserId = Convert.ToInt32(identity.User.UserId);

                if (_CurrentUserId == 0)
                {
                    //becouse Sometime id = 0 ?????!!!! maybe session die???????
                    return View(data);
                }
                Page obj = GetPageSession();
                obj.PageId = data.PageId;
                obj.Title = data.Title;
                obj.PagesContent = data.PagesContent;


                obj.Update_Time = DateTime.Now;//Need solution for this field no need any value.
                obj.UserId = _CurrentUserId;
                
                if (data.PageId == 0)
                {
                    obj.Create_Time = DateTime.Now;
                    
                }
                else
                {
                    obj.Create_Time = data.Create_Time;
                }
                repositoryPage.Save(obj);
                int? Newid = obj.PageId;
                if (obj != null)
                {
                    if (data.PageId == 0)
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
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page model = repositoryPage.Details(Id);
          

            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewPost.chtml to save copy same page 
            return View("AddNewPage", model);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult LastPage()
        {
            IEnumerable<Page> Model;
            Model = repositoryPage.PageList.Distinct();

            return PartialView("_LastPage", Model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Page page = repositoryPage.Details(Id);

            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? Id)
        {
            Page page = repositoryPage.Details(Id);

            Page _page = repositoryPage.Delete(Id);
         
            if (_page != null)
            {
                TempData["message"] = string.Format("{0} удалено", page.Title);
            }
            return RedirectToAction("Index", "Page");
        }
        private Page GetPageSession()
        {
            if (Session["page"] == null)
            {
                Session["page"] = new Page();
            }
            return (Page)Session["page"];
        }



    }
}