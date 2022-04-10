
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
    public class TextController : Controller
    {
        private readonly ITextRepository textRepository;
        public TextController(ITextRepository repo)
        {
            textRepository=repo  ;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? text)
        {
            IEnumerable<Text> model = textRepository.TextList
                .OrderBy(p => p.Id)
                .OrderByDescending(p => p.Create_time)
                .ToPagedList(text ?? 1, 5); 
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
                    Text data = new Text();
                    data.Texts = _txtname;
                    data.Create_time = DateTime.Now;

                     
                    bool result;
                    result= AddText(data);
                    if (result==true)
                    {
                        TempData["message"] = string.Format("Text was Added Successfully");
                    }
                }
            }
            return Json(Convert.ToString(_txtname), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "SuperUser,Admin")]
        public ActionResult NewText()
        {
            Text model = GetTextSession();
            model.Id = 0;//Becouse HttpPost NewPost, Need Id to know Edit or AddNew .

            return View(model);

        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperUser,Admin")]
        public ActionResult NewText(Text data)
        {
            Text obj = GetTextSession();

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
                obj.Texts = data.Texts;


                obj.Create_time = DateTime.Now;//Need solution for this field no need any value.
                obj.UserId = _CurrentUserId;
                if (data.Id == 0)
                {
                    obj.Create_time = DateTime.Now;
                }
                else
                {
                    obj.Create_time = data.Create_time;
                }
                textRepository.Save(obj);
                int? Newid = obj.Id;
                if (obj != null)
                {
                    if (data.Id == 0)
                    {
                        TempData["message"] = string.Format("Text was Added Successfully");
                    }
                    else
                    {
                        TempData["message"] = string.Format("Text was Edited Successfully");
                    }
                }
                return RedirectToAction("");
            }
            return View(data);
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertText(int? Id)
        {
            Text text = textRepository.Details(Id);
            var Temp = $@"<p>{text.Texts}</p>;";

            return Content(Temp);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Text text = textRepository.Details(Id);
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

            Text text = textRepository.Delete(Id);
            if (text != null)
            {
                TempData["message"] = string.Format("deleted");
            }
            return RedirectToAction("Index", "Text");
        }
        [Authorize(Roles = "Admin,SuperUser")]
        public bool AddText(Text data)
        {
            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);

          
           
            bool res=true;

            if (data != null)
            {
                Text obj = GetTextSession();
                obj.Id = data.Id;
                obj.Texts = data.Texts;
                obj.Create_time = data.Create_time;
                obj.UserId = _CurrentUserId;
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

        private Text GetTextSession()
        {
            if (Session["text"] == null)
            {
                Session["text"] = new Text();
            }
            return (Text)Session["text"];
        }


    }
}