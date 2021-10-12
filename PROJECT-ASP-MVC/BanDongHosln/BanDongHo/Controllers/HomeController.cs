﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models.Service;
using BanDongHo.Models.ViewModel;

namespace BanDongHo.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            HomePageViewModel HomePageVM = new HomePageViewModel();
            HomePageVM.ProductsSelling = ProductService.GetListProductsSelling();
            HomePageVM.NewProducts = ProductService.GetListNewProducts().Take(8);
            return View(HomePageVM);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Detail(int? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DetailViewModel detailPage = new DetailViewModel();
            // Lấy sản phẩm
            detailPage.Product = new ProductViewModel();
            detailPage.Product.Product = ProductService.Find(id.Value);
            detailPage.Product.Promotion = PromotionService.GetPromotion(id.Value);
            // Lấy danh sách sản phẩm liên quan
            detailPage.ListProductsRelative = ProductService.GetListProductRelative(detailPage.Product.Product.MATH.Value).Take(3);
            // Lấy danh sách 
            detailPage.Tag = DetailPageService.GetTag(id.Value);
            detailPage.ListNewProducts = ProductService.GetListNewProducts().Take(8);
            return View(detailPage);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel contact)
        {
            // handle in here
            if(ContactService.SendMail(contact))
            {
                RedirectToAction("Index");
            }
            return View(contact);
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}