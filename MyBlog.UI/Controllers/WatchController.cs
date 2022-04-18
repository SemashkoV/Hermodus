
using MyBlog.Data;
using MyBlog.Service;
using MyBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using X.PagedList;

namespace MyBlog.UI.Controllers
{
    public class WatchController : Controller
    {
        private readonly IWatchRepository textRepository;
        public WatchController(IWatchRepository repo)
        {
            textRepository=repo  ;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? text)
        {
            IEnumerable<Watch> model = textRepository.WatchList
                .OrderBy(p => p.Id)
                .ToPagedList(text ?? 1, 5); 
            return View(model);
        }
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
        [Authorize(Roles = "SuperUser,Admin")]
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
                        TempData["message"] = string.Format("Text was Added Successfully");
                    }
                }
            }
            return Json(Convert.ToString(_txtname), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "SuperUser,Admin")]
        public ActionResult NewWatch()
        {
            Watch model = GetWatchSession();
            model.Id = 0;//Becouse HttpPost NewPost, Need Id to know Edit or AddNew .

            return View(model);

        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperUser,Admin")]
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
                obj.Title = data.Title;
                obj.Content = data.Content;
                obj.Image = data.Image;
                obj.Article = data.Article;
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



                textRepository.Save(obj);
                int? Newid = obj.Id;
                if (obj != null)
                {
                    if (data.Id == 0)
                    {
                        TempData["message"] = string.Format("Watch was Added Successfully");
                    }
                    else
                    {
                        TempData["message"] = string.Format("Watch was Edited Successfully");
                    }
                }
                return RedirectToAction("");
            }
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


            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewPost.chtml to save copy same page 
            return View("NewWatch", model);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertWatch(int? Id)
        {
            Watch text = textRepository.Details(Id);
            var Temp = $@"<p>{text.Title}</p>;";

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
                TempData["message"] = string.Format("deleted");
            }
            return RedirectToAction("Index", "Watch");
        }
        [Authorize(Roles = "Admin,SuperUser")]
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
                obj.Article = data.Article;
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