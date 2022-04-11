
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
    public class CSSController : Controller
    {
        private readonly ICSSRepository CSSRepository;
        public CSSController(ICSSRepository repo)
        {
            CSSRepository = repo  ;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? text)
        {
            IEnumerable<CSS> model = CSSRepository.CSSList
                .OrderBy(p => p.Id)
                .OrderByDescending(p => p.Id)
                .ToPagedList(text ?? 1, 5);

            return View(model);
        }
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSS model = CSSRepository.Details(Id);

            // model.UserDetails.FName
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [Authorize(Roles = "SuperUser,Admin")]
        public ActionResult UploadCSS()
        {
            return PartialView("_UploadCSS");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _txtname = string.Empty;

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyCSS"];
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
                    CSS data = new CSS();
                    data.Code = _txtname;

                     
                    bool result;
                    result= AddCSS(data);
                    if (result==true)
                    {
                        TempData["message"] = string.Format("CSS was Added Successfully");
                    }
                }
            }
            return Json(Convert.ToString(_txtname), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "SuperUser,Admin")]
        public ActionResult NewCSS()
        {
            CSS model = GetCSSSession();
            model.Id = 0;//Becouse HttpPost NewPost, Need Id to know Edit or AddNew .

            return View(model);

        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperUser,Admin")]
        public ActionResult NewCSS(CSS data)
        {
            CSS obj = GetCSSSession();

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
                obj.Code = data.Code;

                CSSRepository.Save(obj);
                int? Newid = obj.Id;
                if (obj != null)
                {
                    if (data.Id == 0)
                    {
                        TempData["message"] = string.Format("CSS was Added Successfully");
                    }
                    else
                    {
                        TempData["message"] = string.Format("CSS was Edited Successfully");
                    }
                }
                return RedirectToAction("");
            }
            return View(data);
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertCSS(int? Id)
        {
            CSS css = CSSRepository.Details(Id);
            var Temp = $@"<style>{css.Code}</style>;";

            return Content(Temp);
        }

        [Authorize(Roles = "User,SuperUser,Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSS model = CSSRepository.Details(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewComment page.chtml to save copy same page 
            return View("NewCSS", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            CSS css = CSSRepository.Details(Id);
            if (css == null)
            {
                return HttpNotFound();
            }
            return View(css);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int? Id)
        {

            CSS css = CSSRepository.Delete(Id);
            if (css != null)
            {
                TempData["message"] = string.Format("deleted");
            }
            return RedirectToAction("Index", "CSS");
        }
        [Authorize(Roles = "Admin,SuperUser")]
        public bool AddCSS(CSS data)
        {
            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);

          
           
            bool res=true;

            if (data != null)
            {
                CSS obj = GetCSSSession();
                obj.Id = data.Id;
                obj.Code = data.Code;
                CSSRepository.Save(obj);
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

        private CSS GetCSSSession()
        {
            if (Session["css"] == null)
            {
                Session["css"] = new CSS();
            }
            return (CSS)Session["css"];
        }


    }
}