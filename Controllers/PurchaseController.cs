using Multilinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Multilinks.Controllers
{
	public class PurchaseController : Controller
	{
		Model1 db = new Model1();
	
		public ActionResult Purchase(int? id)
		{
			ShopModel s = new ShopModel();

			s.Cat = db.Categories.ToList();
			if (id == null)

				s.Pro = db.Products.ToList();
			else
				s.Pro = db.Products.Where(p => p.Category_FID == id).ToList();

			return View(s);
		}		
		public ActionResult AddtoCart(int id)
        {
			List<Product> list;
			if (Session["MyPurchase"] == null)
			{ list = new List<Product>();
			}
			else
			{ list = (List<Product>)Session["MyPurchase"]; }
			list.Add(db.Products.Where(p => p.Product_ID == id).FirstOrDefault());
			Session["MyPurchase"] = list;
			list[list.Count - 1].Pro_Quantity = 1;
			ViewBag.cart = list.Count();
			Session["Pcount"] = Convert.ToInt32(Session["Pcount"]) + 1;
			return Json("Your products are added to cart", JsonRequestBehavior.AllowGet);
		}
		public ActionResult MinusFromCart(int Rowno)
        {
		List<Product>	list = (List<Product>)Session["MyPurchase"];
		    list[Rowno].Pro_Quantity--;
			Session["MyPurchase"] = list;
			if(list[Rowno].Pro_Quantity<1)
            {
				list.RemoveAt(Rowno);
				Session["Pcount"] = Convert.ToInt32(Session["Pcount"]) - 1;
			}
			return RedirectToAction("PurchaseCart");
		}
		public ActionResult PlustoCart(int Rowno)
		{
			List<Product> list = (List<Product>)Session["MyPurchase"];
			list[Rowno].Pro_Quantity++;
			Session["MyPurchase"] = list;
			return RedirectToAction("PurchaseCart");
		}
		public ActionResult RemoveFromCart(int Rowno)
		{
			List<Product> list = (List<Product>)Session["MyPurchase"];
			list.RemoveAt(Rowno);
			Session["MyPurchase"] = list;
			Session["Pcount"] = Convert.ToInt32(Session["Pcount"])-1;
			return RedirectToAction("PurchaseCart");
		}

		public ActionResult PurchaseCart()
        {
		if (TempData.ContainsKey("itemnotadd") && (bool)TempData["itemnotadd"])
			{
				ViewBag.Message = "Please Add Items to cart For Checkout";
			}
			return View();
		}
		public ActionResult PurchaseCheckout()
		{
			if (Session["PTotalamount"] == null || ((int)Session["Pcount"]) == 0)
			{
				TempData["itemnotadd"] = true;
				return RedirectToAction("PurchaseCart");

			}
			else
			{
				return View();
			}
		}

		public ActionResult Additemtostock(Order o)
		{
			o.Order_Date = System.DateTime.Now;
			o.Order_Status = "Paid";
			o.Order_Type = "Purchase";
			Session["POrder"] = o;
			return RedirectToAction("SavingData");
		}

		public ActionResult SavingData()
        {
			Order o = (Order)Session["POrder"];			
			//Save Order in databasw
			db.Orders.Add(o);
			db.SaveChanges();
			//OrderDetail
			List<Product> L = (List<Product>)Session["MyPurchase"];
			for (int i = 0; i < L.Count;i++)
            {
				OrderDetail od = new OrderDetail();
				int orderid = db.Orders.Max(x => x.Order_ID);
				od.Order_FID = orderid;
				od.Product_FID = L[i].Product_ID;
				od.Purchase_Price = L[i].Product_PurchasePrice;
				od.Quantity = L[i].Pro_Quantity;
				od.Sale_Price = L[i].Product_SalePrice;
				db.OrderDetails.Add(od);
				db.SaveChanges();

			}

			return RedirectToAction("PurchaseOrderBooked");
        }
		public ActionResult PurchaseOrderBooked()
        {
			return View();
        }

	}
}