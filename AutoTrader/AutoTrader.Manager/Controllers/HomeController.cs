using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoTrader.Manager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ItemsWithoutProduct()
        {
            using (var db = new AutoTrader.Data.TraderEntities())
            {
                var model = db.Item.Where(o => !o.Product.HasValue).ToList();
                return View(model);
            }
        }
    }
}