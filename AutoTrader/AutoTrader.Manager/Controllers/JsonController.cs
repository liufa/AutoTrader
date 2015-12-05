using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoTrader.Manager.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        public JsonResult LookupProduct(string name)
        {
            using (var db = new Data.TraderEntities())
            {
                var model = db.Product.Where(o => o.Name == name).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
    }
}