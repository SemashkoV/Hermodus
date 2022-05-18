﻿
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
    public class CartController : Controller
    {
        private IWatchRepository repository;
        public CartController(IWatchRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public RedirectToRouteResult AddToCart(int Id, string returnUrl)
        {
            Watch watch = repository.WatchList
                .FirstOrDefault(g => g.Id == Id);

            if (watch != null)
            {
                GetCart().AddItem(watch, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int Id, string returnUrl)
        {
            Watch watch = repository.WatchList
                .FirstOrDefault(g => g.Id == Id);

            if (watch != null)
            {
                GetCart().RemoveLine(watch);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}