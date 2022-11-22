using Multilinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multilinks.Controllers
{
    public class ReportController : Controller
    {
        Model1 db = new Model1();
        public ActionResult PurchaseReport(FilterModel fm)
        {
            if (fm.DateFrom != null)

            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }
            if (fm.DateTo != null)
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_ID.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.Category_FID == fm.Category).Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            var od = db.OrderDetails.Select(x => x.Order_FID).ToList();
            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.Category_FID == fm.Category).Select(x => x.Product_ID).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.Product_ID == fm.Product && x.Category_FID == fm.Category).Select(x => x.Product_ID).ToList();
                }

                od = db.OrderDetails.Where(x => p.Contains((int)x.Product_FID)).Select(x => x.Order_FID).ToList();
            }


            if (fm.DateFrom == null && fm.Product == null && fm.Category == null)
            {
                var s = db.Orders.Where(x => x.Order_Type == "Purchase").OrderByDescending(x => x.Order_Date).ToList();
                return View(s);
            }
            else
            {
                var s = db.Orders.Where(x => x.Order_Type == "Purchase" && x.Order_Date >= fm.DateFrom & x.Order_Date <= fm.DateTo & od.Contains(x.Order_ID)).OrderByDescending(x => x.Order_Date).ToList();
                return View(s);
            }
        }
        public ActionResult SaleReport(FilterModel fm)
        {
            if (fm.DateFrom != null)

            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }
            if (fm.DateTo != null)
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_ID.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            else
            {            
            ViewBag.Product = db.Products.Where(x=>x.Category_FID==fm.Category).Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            var od = db.OrderDetails.Select(x => x.Order_FID).ToList();
            if(fm.Category!=null)
            {
                var p = db.Products.Where(x => x.Category_FID == fm.Category).Select(x => x.Product_ID).ToList();
                if(fm.Product!=null)
                {
                    p=db.Products.Where(x=>x.Product_ID==fm.Product && x.Category_FID == fm.Category).Select(x => x.Product_ID).ToList();
                }
                
                od = db.OrderDetails.Where(x => p.Contains((int)x.Product_FID)).Select(x => x.Order_FID).ToList();            
            }
           

            if (fm.DateFrom == null && fm.Product==null && fm.Category==null)
            {
                var s = db.Orders.Where(x => x.Order_Type == "Sale").OrderByDescending(x => x.Order_Date).ToList();
                return View(s);
            }
            else
            {
                var s = db.Orders.Where(x => x.Order_Type == "Sale" && x.Order_Date >= fm.DateFrom & x.Order_Date <= fm.DateTo & od.Contains(x.Order_ID)).OrderByDescending(x => x.Order_Date).ToList();
                return View(s);
            }
        }
        public ActionResult Invoice(int id)
        {
            var s = db.Orders.Where(x => x.Order_ID==id).ToList();
            return View(s);
        }
        public ActionResult ProfitandLossreport(FilterModel fm)
        {
            if (fm.DateFrom != null)

            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }
            if (fm.DateTo != null)
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_ID.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.Category_FID == fm.Category).Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            var od = db.OrderDetails.Select(x => x.Order_FID).ToList();
            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.Category_FID == fm.Category).Select(x => x.Product_ID).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.Product_ID == fm.Product && x.Category_FID == fm.Category).Select(x => x.Product_ID).ToList();
                }

                od = db.OrderDetails.Where(x => p.Contains((int)x.Product_FID)).Select(x => x.Order_FID).ToList();
            }


            if (fm.DateFrom == null && fm.Product == null && fm.Category == null)
            {
                var s = db.Orders.Where(x => x.Order_Type == "Sale").OrderByDescending(x => x.Order_Date).ToList();
                return View(s);
            }
            else
            {
                var s = db.Orders.Where(x => x.Order_Type == "Sale" && x.Order_Date >= fm.DateFrom & x.Order_Date <= fm.DateTo & od.Contains(x.Order_ID)).OrderByDescending(x => x.Order_Date).ToList();
                return View(s);
            }
        }
        public ActionResult StockReport(FilterModel fm)
        {
            if (fm.DateTo != null)
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_ID.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.Category_FID == fm.Category).Select(x => new SelectListItem { Value = x.Product_ID.ToString(), Text = x.Product_Name }).ToList();

            }
            var p = db.Products.ToList();
            if (fm.Category != null)
            {
                 p = db.Products.Where(x => x.Category_FID == fm.Category).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.Product_ID == fm.Product && x.Category_FID == fm.Category).ToList();
                }

           }
            if (fm.DateTo == null && fm.Product == null && fm.Category == null)
            {
                return View(p);
            }
            else
            {
                return View(p);
            }
            
        }
    }
}