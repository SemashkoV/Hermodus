
using Hermodus.Data;
using Hermodus.Service;
using Hermodus.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using X.PagedList;


namespace Hermodus.UI.Controllers
{
    public class ShippingDetailController : Controller
    {
        private readonly IShippingDetailRepository repositoryCommment;

        public ShippingDetailController(IShippingDetailRepository repoComment)
        {
            repositoryCommment = repoComment;
        }
        // GET: Comment
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            IEnumerable<ShippingDetail> model = repositoryCommment.OrdersIEnum.OrderBy(p => p.Id)
            .OrderByDescending(p => p.Create_time).ToPagedList(page ?? 1, 5);//5 is pagesize
         /*
          * //Fututer Plan to Delete All SuperUser ActionResult ,
          *  Check RoleId after send him to page only
           var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserRole = Convert.ToInt32(identity.User.RoleId);
             */

            return View(model);
        }
     
      
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddNewOrder()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddNewOrder(int? Id,ShippingDetail data)
        {

            var identity = (HttpContext.User as MyPrincipal).Identity as  MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);





            ShippingDetail obj = GetShippingDetailSession();
                
                obj.Id = data.Id;
           
                obj.Name = data.Name;

               
                obj.Create_time = DateTime.Now;//Need solution for this field no need any value.
               
                obj.Phone = data.Phone;//PostId
                obj.Mail = data.Mail;
                obj.Address = data.Address;
                obj.Comment = data.Comment;
                obj.Cart = data.Cart;
                if (obj.Id == 0)
                {
                    obj.Publish = false; //New Commnet need aprove(Dash) to publish it
                    obj.Create_time = DateTime.Now;
                    
     
                }

                else {
                    obj.Create_time = data.Create_time;
                    obj.Publish = false; //Edit  Commnet need aprove(Dash) to publish it ,again
                }
                
                repositoryCommment.Save(obj);
                int? Newid = obj.Id;
                
                return RedirectToAction("ClearCart", "Cart");
           
        }
        [Authorize(Roles = "User,Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDetail model = repositoryCommment.Details(Id);
           
            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewComment page.chtml to save copy same page 
            return View("AddNewComment", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            ShippingDetail _Comment = repositoryCommment.Details(Id);

            if (_Comment == null)
            {
                return HttpNotFound();
            }
            return View(_Comment);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? Id)
        {

            ShippingDetail _Comment = repositoryCommment.Delete(Id);
           
            if (_Comment != null)
            {
                TempData["message"] = string.Format("deleted");
            }
            return RedirectToAction("Index", "ShippingDetail");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OrderNeedAprove(int? page)
        { 
            IEnumerable<ShippingDetail> model = repositoryCommment.OrdersList
                .Where(p => p.Publish == false)//just what need aprove
                .OrderBy(p => p.Id)
           .OrderByDescending(p => p.Create_time).ToPagedList(page ?? 1, 5);//5 is pagesize
            
            return View(model); 
        }
        
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PublishComment(int? Id)
        {
            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserRole = Convert.ToInt32(identity.User.RoleId);

            ShippingDetail _Comment = repositoryCommment.Details(Id);
            _Comment.Publish = true;//Aprove 
            repositoryCommment.Save(_Comment);
            
                TempData["message"] = string.Format(" Published Successfully");

            if (_CurrentUserRole == 1) { 
             return RedirectToAction("OrderNeedAprove", "ShippingDetail");
            }
            else
            {
                return RedirectToAction("OrderNeedAprove", "ShippingDetail");
            }
        }
              

           
        
        ///Sessions
        ///
        private ShippingDetail GetShippingDetailSession()
        {
            if (Session["shippingdetail"] == null)
            {
                Session["shippingdetail"] = new ShippingDetail();
            }
            return (ShippingDetail)Session["shippingdetail"];
        }
    }
}