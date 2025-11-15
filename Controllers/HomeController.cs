using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestDeAn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Category()
        {
            return View();
        }

        public ActionResult DeliverLocate()
        {
            return View();
        }
        public ActionResult OrderHistory()
        {
            return View();
        }
        public ActionResult OrderDetail()
        {
            return View();
        }
        public ActionResult Payment_Confirm()
        {
            return View();
        }
        public ActionResult Payment_List()
        {
            return View();
        }
        public ActionResult DetailProduct_White()
        {
            return View();
        }
        public ActionResult DetailProduct_Purple()
        {
            return View();
        }
        public ActionResult DetailProduct_Black()
        {
            return View();
        }
        public ActionResult DetailProduct_Orange()
        {
            return View();
        }
        public ActionResult Register_Login()
        {
            return View();
        }
        public ActionResult Delivery()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}