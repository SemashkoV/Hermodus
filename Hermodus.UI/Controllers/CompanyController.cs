
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
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository textRepository;
        public CompanyController(ICompanyRepository repo)
        {
            textRepository=repo  ;
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? text)
        {
            IEnumerable<Company> model = textRepository.CompanyList
                .OrderBy(p => p.Id)
                .ToPagedList(text ?? 1, 5); 
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company model = textRepository.Details(Id);

            // model.UserDetails.FName
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
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
                    Company data = new Company();
                    data.Name = _txtname;
                    

                     
                    bool result;
                    result= AddCompany(data);
                    if (result==true)
                    {
                        TempData["message"] = string.Format("Добавлено успешно");
                    }
                }
            }
            return Json(Convert.ToString(_txtname), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult NewCompany()
        {
            Company model = GetCompanySession();
            model.Id = 0;//Because HttpPost NewPost, Need Id to know Edit or AddNew .

            return View(model);

        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult NewCompany(Company data)
        {
            Company obj = GetCompanySession();

            if (ModelState.IsValid)
            {

                var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
                int _CurrentUserId = Convert.ToInt32(identity.User.UserId);
                if (_CurrentUserId == 0)
                {
                    //because Sometime id = 0 ?????!!!! maybe session die???????
                    return View(data);
                }

                obj.Id = data.Id;
                obj.Name = data.Name;
                obj.Country = data.Country;



                textRepository.Save(obj);
                int? Newid = obj.Id;
                if (obj != null)
                {
                    if (data.Id == 0)
                    {
                        TempData["message"] = string.Format("Добавлено успешно");
                    }
                    else
                    {
                        TempData["message"] = string.Format("Добавлено успешно");
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
            Company model = textRepository.Details(Id);


            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewPost.chtml to save copy same page 
            return View("NewCompany", model);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult InsertCompany(int? Id)
        {
            Company text = textRepository.Details(Id);
            var Temp = $@"<p>{text.Name}</p>;";

            return Content(Temp);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Company text = textRepository.Details(Id);
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

            Company text = textRepository.Delete(Id);
            if (text != null)
            {
                TempData["message"] = string.Format("удалено");
            }
            return RedirectToAction("Index", "Company");
        }
        [Authorize(Roles = "Admin")]
        public bool AddCompany(Company data)
        {
            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);

          
           
            bool res=true;

            if (data != null)
            {
                Company obj = GetCompanySession();

                obj.Id = data.Id;
                obj.Name = data.Name;
                obj.Country = data.Country;


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

        private Company GetCompanySession()
        {
            if (Session["company"] == null)
            {
                Session["company"] = new Company();
            }
            return (Company)Session["company"];
        }


    }
}