using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace AutoTrader.Manager.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        public JsonResult LookupProduct(string name)
        {
            using (var db = new Data.TraderEntities())
            {
                var model = db.Product.Where(o => o.Name.Contains(name)).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddProduct(string name)
        {
            using (var db = new Data.TraderEntities())
            {
                if (!db.Product.Any(o => o.Name == name))
                {
                    db.Product.Add(new Data.Product { Name = name });
                    db.SaveChanges();
                }
                var item = db.Product.Include(o => o.ProductKeywords)
                    .Single(o => o.Name == name);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
        }
    }
}