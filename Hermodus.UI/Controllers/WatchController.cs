﻿
using Hermodus.Data;
using Hermodus.Service;
using Hermodus.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using X.PagedList;

namespace Hermodus.UI.Controllers
{
    public class WatchController : Controller
    {
        private readonly IWatchRepository textRepository;
        private readonly ICompanyRepository companyRepository;
        public WatchController(IWatchRepository repo, ICompanyRepository comprepo)
        {
            textRepository=repo  ;
            companyRepository=comprepo ;
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            IEnumerable<Watch> model = textRepository.WatchList
                .OrderBy(p => p.Id)
                .ToPagedList(page ?? 1, 10); 
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Watch model = textRepository.Details(Id);

            // model.UserDetails.FName
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UploadText()
        {
            return PartialView("_UploadText");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _txtname = string.Empty;

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyTexts"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _txtname = Guid.NewGuid().ToString();
                   
                    var _comPath = Server.MapPath("/Upload/Id_") + _txtname + _ext;
                    _txtname = "Id_" + _txtname + _ext;
                  
                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                    var _lenght = new FileInfo(path).Length;
                    //here to add Image Path to You Database ,
                    Watch data = new Watch();
                    data.Title = _txtname;
                    

                     
                    bool result;
                    result= AddWatch(data);
                    if (result==true)
                    {
                        TempData["message"] = string.Format("Добавлено успешно");
                    }
                }
            }
            return Json(Convert.ToString(_txtname), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult NewWatch()
        {
            Watch model = GetWatchSession();
            model.Id = 0;//Becouse HttpPost NewPost, Need Id to know Edit or AddNew .
            model.CompanyDetails = companyRepository.CompanyIEnum;
            return View(model);

        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult NewWatch(Watch data)
        {
            Watch obj = GetWatchSession();

            if (ModelState.IsValid)
            {

                var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
                int _CurrentUserId = Convert.ToInt32(identity.User.UserId);
                if (_CurrentUserId == 0)
                {
                    //becouse Sometime id = 0 ?????!!!! maybe session die???????
                    return View(data);
                }

                obj.Id = data.Id;
                obj.CompanyId = data.CompanyId;
             
                obj.Model = data.Model;
                obj.Content = data.Content;
                obj.Image = data.Image;
                obj.Article = Convert.ToString(data.Id);
                obj.Country = data.Country;
                obj.Movement = data.Movement;
                obj.Frame = data.Frame;
                obj.Face = data.Face;
                obj.Bracelet = data.Bracelet;
                obj.Protection = data.Protection;
                obj.Backlight = data.Backlight;
                obj.Glass = data.Glass;
                obj.Calendar = data.Calendar;
                obj.Size = data.Size;
                obj.Price = data.Price;
                obj.Title = data.Title;

                textRepository.Save(obj);
                int? Newid = obj.Id;
                if (obj != null)
                {
                    if (data.Id == 0)
                    {
                        TempData["message"] = string.Format("Модель добавлена успешно");
                    }
                    else
                    {
                        TempData["message"] = string.Format("Модель изменена успешно");
                    }
                }
                return RedirectToAction("");
            }
            data.CompanyDetails = companyRepository.CompanyIEnum;
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Watch model = textRepository.Details(Id);
            model.CompanyDetails = companyRepository.CompanyIEnum;

            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewPost.chtml to save copy same page 
            return View("NewWatch", model);
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertWatchModel(int? Id)
        {
            Watch text = textRepository.Details(Id);
            var Temp = text.Model;

            return Content(Temp);
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertWatchCompany(int? Id)
        {
            Watch text = textRepository.Details(Id);
            var Temp = $"{text.CompanyId}";

            return Content(Temp);
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertWatchImg(int? Id)
        {
            Watch text = textRepository.Details(Id);
            var Temp = text.Image;

            return Content(Temp);
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertWatchPrice(int? Id)
        {
            Watch text = textRepository.Details(Id);
            var Temp = text.Price.ToString();

            return Content(Temp);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Watch text = textRepository.Details(Id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int? Id)
        {

            Watch text = textRepository.Delete(Id);
            if (text != null)
            {
                TempData["message"] = string.Format("удалено");
            }
            return RedirectToAction("Index", "Watch");
        }
        [Authorize(Roles = "Admin")]
        public bool AddWatch(Watch data)
        {
            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);

          
           
            bool res=true;

            if (data != null)
            {
                Watch obj = GetWatchSession();

                obj.Id = data.Id;
                obj.Title = data.Title;
                obj.Content = data.Content;
                obj.Image = data.Image;
                obj.Article = Convert.ToString(data.Id);
                obj.Country = data.Country;
                obj.Movement = data.Movement;
                obj.Frame = data.Frame;
                obj.Face = data.Face;
                obj.Bracelet = data.Bracelet;
                obj.Protection = data.Protection;
                obj.Backlight = data.Backlight;
                obj.Glass = data.Glass;
                obj.Calendar = data.Calendar;
                obj.Size = data.Size;
                obj.Price = data.Price;

                textRepository.Save(obj);
                int? Newid = obj.Id;
               
                 res = true;
            }
            else
            {
                 res = false;
            }
            return res;
        }



        ///Sessions
        ///

        private Watch GetWatchSession()
        {
            if (Session["watch"] == null)
            {
                Session["watch"] = new Watch();
            }
            return (Watch)Session["watch"];
        }


    }
}