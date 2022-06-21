using Hermodus.Data;
using Hermodus.Service;
using Hermodus.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace Hermodus.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingRepository repositorySetting;
        private readonly IEmailSettingRepository repositoryEmailSetting;
        private readonly IDEncryptionRepository repositoryDEncrption;
        public SettingController(ISettingRepository repoSetting,
                                 IEmailSettingRepository repoEmailSetting,
                                 IDEncryptionRepository repoDEncrption)
        {
            repositorySetting = repoSetting;
            repositoryEmailSetting = repoEmailSetting;
            repositoryDEncrption = repoDEncrption;
        }
        // GET: Setting
       
        public ActionResult Index()
        {
            return View();
        }
        //This Action Should Run once only Install time .
        public ActionResult CreateSetting()
        {
            Setting model = new Setting();
            model.Id = 0;
            model.Update_Time = DateTime.Now.Date;
            return View("UpdateSetting", model);
        }
        [HttpPost]
        public ActionResult UpdateSetting(Setting data )
        {
            Setting obj = GetSettingSession();

            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);
            if (_CurrentUserId == 0)
            {
                //becouse Sometime id = 0 ?????!!!! maybe session die???????
                return View(data);
            }
            if (ModelState.IsValid)
            {
                obj.Id = data.Id;
                obj.HomeImageText = data.HomeImageText;
                obj.HomeImage1 = data.HomeImage1;
                obj.HomeImage2 = data.HomeImage2;
                obj.HomeImage3 = data.HomeImage3;
                obj.NumberOfLastPost = data.NumberOfLastPost;
                obj.NumberOfCategory = data.NumberOfCategory;
                obj.PostNumberInPage = data.PostNumberInPage;
                obj.NumberOfTopPost = data.NumberOfTopPost;
                obj.Update_Time = DateTime.Now;
                obj.UserId = _CurrentUserId;
                //obj.DisplayLastCategory = data.DisplayLastCategory;
                //obj.DisplayLastPost = data.DisplayLastPost;
                //obj.DisplayFbWidget = data.DisplayFbWidget;
                //obj.DisplayTwWidget = data.DisplayTwWidget;
                //obj.DisplayGoogleWidget = data.DisplayGoogleWidget;
                //obj.FBAppID = data.FBAppID;
                //obj.FBAppSecret = data.FBAppSecret;
                //obj.GoogleSitekey = data.GoogleSitekey;
                //obj.GoogleSecretkey = data.GoogleSecretkey;
                repositorySetting.Save(obj);
                if (obj != null)
                {
                    if (data.Id == 0)
                    {
                        TempData["message"] = string.Format("Добавлено успешно");
                    }
                    else
                    {
                        TempData["message"] = string.Format("Изменено успешно");
                    }
                }
                return RedirectToAction("Details", "Setting",data);//SamePlace
            }

            return View(data);
        }
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting model = repositorySetting.Details(Id);

            // model.UserDetails.FName
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("UpdateSetting",model);
        }
        //This Action Should Run once only Install time ........******//////
        public ActionResult CreateEMailSetting()
        {
            EmailSetting model = new EmailSetting();
            model.Id = 0;
            model.Last_Update = DateTime.Now.Date;
            return View("UpdateEMailSetting", model);
        }
        [HttpPost]
        public ActionResult UpdateEmailSetting(EmailSetting data)
        {
            EmailSetting obj = GetEmailSettingSession();

            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);
            if (_CurrentUserId == 0)
            {
                //becouse Sometime id = 0 ?????!!!! maybe session die???????
                return View(data);
            }
            if (ModelState.IsValid)
            {
                obj.Id = data.Id;
                obj.SMTP_Server = data.SMTP_Server;
                obj.Sender = data.Sender;
                obj.SMTPServer_Port = data.SMTPServer_Port;
                obj.UserName = data.UserName;
                string HashPassword = repositoryDEncrption.Encrypt(data.Password);
                obj.Password = HashPassword;
                obj.EnableSSL = data.EnableSSL;
                obj.Last_Update = DateTime.Now;
                obj.UserId = _CurrentUserId;
                
                repositoryEmailSetting.Save(obj);
                if (obj != null)
                {
                    if (data.Id == 0)//New or Update
                    {
                        TempData["message"] = string.Format("Добавлено успешно");
                    }
                    else
                    {
                        TempData["message"] = string.Format("Изменено успешно");
                    }
                }
                return RedirectToAction("EmailSettingDetails", "Setting", data);//SamePlace
            }

            return View(data);
        }
        
        //ReadSetting From  webconfig
        public string  ReadSetting(string key)
        {
           
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
            return result;
            
        }

        static void AddUpdateAppSettings(string key, string value)
        {
          
                var configFile = WebConfigurationManager.OpenWebConfiguration("~/");
              var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            
        

        public ActionResult EmailSettingDetails(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailSetting model = repositoryEmailSetting.Details(Id);
           

            // model.UserDetails.FName
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Password = repositoryDEncrption.Decrypt(model.Password);
            return View("UpdateEMailSetting", model);
        }
        ///Sessions
        ///
        private Setting GetSettingSession()
        {
            if (Session["setting"] == null)
            {
                Session["setting"] = new Setting();
            }
            return (Setting)Session["setting"];
        }
        private EmailSetting GetEmailSettingSession()
        {
            if (Session["Emailsetting"] == null)
            {
                Session["Emailsetting"] = new EmailSetting();
            }
            return (EmailSetting)Session["Emailsetting"];
        }

 

    }
}