using AutoTrader.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AutoTrader.Manager.Controllers
{
    public class JsonController : Controller
    {
        public JsonResult LookupProduct(string name)
        {
            var model = new List<Product>();
            using (var db = new Data.TraderEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                model = db.Product.Where(o => o.Name.Contains(name)).ToList();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
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

        public JsonResult MapProduct(int itemId, int productId)
        {
            using (var db = new Data.TraderEntities())
            {
                var product = db.Product.SingleOrDefault(o => o.Id == productId);
                var item = db.Item.SingleOrDefault(o => o.Id == itemId);
                if (product!=null && item!=null)
                {
                    item.Product = productId;
                    db.SaveChanges();
                }
                var model = db.Item.Include(o => o.Product1).Include(o => o.Product1.ProductKeywords)
                    .Single(o => o.Id == itemId);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UnmapProduct(int itemId, int productId)
        {
            using (var db = new Data.TraderEntities())
            {
                var product = db.Product.SingleOrDefault(o => o.Id == productId);
                var item = db.Item.SingleOrDefault(o => o.Id == itemId);
                if (product != null && item != null)
                {
                    item.Product = null;
                    db.SaveChanges();
                }

                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LookupKeyword(string name)
        {
            var model = new List<ProductKeywords>();
            using (var db = new Data.TraderEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                model = db.ProductKeywords.Where(o => o.Keyword.Contains(name)).ToList();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddKeyword(string name, int productId)
        {
            using (var db = new Data.TraderEntities())
            {
                if (!db.ProductKeywords.Any(o => o.Keyword == name && o.Product == productId))
                {
                    db.ProductKeywords.Add(new Data.ProductKeywords { Keyword = name, Product = productId });
                    db.SaveChanges();
                }

                var item = db.ProductKeywords.Single(o => o.Keyword == name && o.Product == productId);
                return RedirectToAction("Product", "Home", new { id = productId });
            }
        }
        
        public ActionResult DeleteKeyword(int keywordId)
        {
            int? productId;
            using (var db = new Data.TraderEntities())
            {
                var keyword = db.ProductKeywords.SingleOrDefault(o => o.Id == keywordId);
                productId = keyword.Product;
                if (keyword != null)
                {
                    db.ProductKeywords.Remove(keyword);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Product", "Home", new { id = productId });
        }
    }
}