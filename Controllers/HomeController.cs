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
	public class HomeController : Controller
	{
		Model1 db = new Model1();
		public ActionResult Indexcustomer()
		{
			var c=db.Categories.ToList();
			return View(c);
		}
		public ActionResult Test()
		{
			return View();
		}
		public ActionResult IndexAdmin()
		{
			return View();
		}


		public ActionResult About()
		{
			
			return View();
		}
		public ActionResult Contact()
		{
			return View();
		}
		public ActionResult ContactForm(Contact c)
		{
			db.Contacts.Add(c);
			db.SaveChanges();
			return Json("Your Message Sent Successfully",JsonRequestBehavior.AllowGet);
		}

		public ActionResult AdminLogin()
		{

			return View();
		}
		[HttpPost]
		public ActionResult AdminLogin(Admin admin)
		{  
           Admin a= db.Admins.Where(x => x.Admin_Email == admin.Admin_Email && x.Admin_Password == admin.Admin_Password).FirstOrDefault();
			if(a!=null)
            {
				Session["Adminlogin"] = a;
				return RedirectToAction("IndexAdmin", "Home");
            }
			else
            {
				ViewBag.Message = "Invalid Email and Password";
				return View();
            }
		}

		public ActionResult Shop(int? id)
		{
			ShopModel s = new ShopModel();

			s.Cat = db.Categories.ToList();
			if (id == null)

				s.Pro = db.Products.ToList();
			else
				s.Pro = db.Products.Where(p => p.Category_FID == id).ToList();

			return View(s);
		}
		public ActionResult ProductDetail(int id)
		{
			
			return View(db.Products.Where(p => p.Product_ID == id).ToList());
		}
		public ActionResult AddtoCart(int id)
        {
			List<Product> list;
			if (Session["MyCart"] == null)
			{ list = new List<Product>();
			}
			else
			{ list = (List<Product>)Session["MyCart"]; }
			Boolean isProductExist = false;
				foreach (var item in list)
				{
					if (item.Product_ID == id)
					{
					isProductExist = true;
					item.Pro_Quantity++;
					}
				
				}
			if (isProductExist != true)
			{
				list.Add(db.Products.Where(p => p.Product_ID == id).FirstOrDefault());
				list[list.Count - 1].Pro_Quantity = 1;
				Session["count"] = Convert.ToInt32(Session["count"]) + 1;
			}
			Session["MyCart"] = list;
			return RedirectToAction("Shop");
		}
		public ActionResult MinusFromCart(int Rowno)
        {
		List<Product>	list = (List<Product>)Session["MyCart"];
		    list[Rowno].Pro_Quantity--;
			Session["MyCart"] = list;
			if(list[Rowno].Pro_Quantity<1)
            {
				list.RemoveAt(Rowno);
				Session["count"] = Convert.ToInt32(Session["count"]) - 1;
			}
			return RedirectToAction("Cart");
		}
		public ActionResult PlustoCart(int Rowno)
		{
			List<Product> list = (List<Product>)Session["MyCart"];
			int pid = list[Rowno].Product_ID;
			int? available = db.OrderDetails.Where(x => x.Product_FID == pid && x.Order.Order_Type == "Purchase").Sum(x => x.Quantity) - db.OrderDetails.Where(x => x.Product_FID == pid && x.Order.Order_Type == "Sale").Sum(x => x.Quantity);
			if (available > list[Rowno].Pro_Quantity)
			{
				list[Rowno].Pro_Quantity++;
				Session["MyCart"] = list;
				return RedirectToAction("Cart");
			}
			else
            {
				TempData["You Have added Maximum Stock"] = true;
				return RedirectToAction("Cart");
			}
			
		}
		public ActionResult RemoveFromCart(int Rowno)
		{
			List<Product> list = (List<Product>)Session["MyCart"];
			list.RemoveAt(Rowno);
			Session["MyCart"] = list;
			Session["count"] = Convert.ToInt32(Session["count"])-1;
			return RedirectToAction("Cart");
		}

		public ActionResult Cart()
        {
		if (TempData.ContainsKey("itemnotadd") && (bool)TempData["itemnotadd"])
			{
				ViewBag.Message = "Please Add Items to cart For Checkout";
			}
		if (TempData.ContainsKey("You Have added Maximum Stock") && (bool)TempData["You Have added Maximum Stock"])
			{
				ViewBag.Message = "You Cannot add more items";
			}
			return View();
		}
		public ActionResult Checkout()
		{		
			if (Session["Totalamount"] == null || ((int)Session["count"]) == 0)
			{
				TempData["itemnotadd"] = true;
				return RedirectToAction("Cart");

			}
			if(Session["Customer"]==null)
            {
				return RedirectToAction("CustomerSignup","Customers");
            }
			else
			{
				return View();
			}
		}

		public ActionResult Paynow(Order o)
		{
			Customer c = (Customer)Session["Customer"];
			o.Order_Date = System.DateTime.Now;
			o.Order_Status = "Pending";
			o.Order_Type = "Sale";
			o.Customer_FID = c.Customer_ID;
			Session["Order"] = o;
			if (Session["MyCart"] != null && ((int)Session["count"]) != 0 && o.Order_PaymentMode == "Paypal")
			{

				return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=Multilinks@business.example.com&return=https://localhost:44385/Home/Savingdata&item_name=Multilinks&amount=" + double.Parse(Session["Totalamount"].ToString()) / 176);

			}
			else if (Session["MyCart"] != null && ((int)Session["count"]) != 0 && o.Order_PaymentMode == "Cash on Delivery")
			{
				return RedirectToAction("Savingdata");
			}
			return View();
		}

		public ActionResult Savingdata()
        {
			Order o = (Order)Session["Order"];
			//Email REmainder
			//	MailMessage message = new MailMessage();
			//	message.From= new MailAddress("Kamranalizx491@gmail.com");
			//	message.To.Add(new MailAddress(o.Order_Email));
			//	message.Subject = "Order Confirmation";
			//	message.IsBodyHtml = true; //to make message body as html  
			//	message.Body = "<b>Multilinks</b><br> Thanks For Order.Your Order Will be deliverd in 5 Working Days";

			//	SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
			//	SmtpServer.Port = 587;
			//	SmtpServer.EnableSsl = true;
			//	SmtpServer.Credentials = new System.Net.NetworkCredential("Kamranalizx491@gmail.com", "laptop@@@786@@@");
			//	SmtpServer.Send(message);

			//Sms ALert
			//String api = "https://lifetimesms.com/json?api_token=9e6f33670b3b23a9c09e9958f958e923861a736994&api_secret=UNIPROJECT&to" + o.Order_Contact + "&from=UNIPROJECT&message=Order Confirmation. Thanks........";
			//HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);
			//var httpResponse = (HttpWebResponse)req.GetResponse();
			//String api = "https://lifetimesms.com/json?api_token=028be5ad3e05996e1cbd97c34d85e9883491296846&api_secret=Multilinks&to=" + o.Order_Contact + "&from=Multilinks&message=<b>Multilinks </b>Thanks For Order..Your Order Will Deliver Soon..For Any Query Please COontact/ 03049356118";
			//	HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);
			// var httpResponse = (HttpWebResponse)req.GetResponse();
			//Save Order in databasw
			db.Orders.Add(o);
			db.SaveChanges();

			//OrderDetail
			List<Product> L = (List<Product>)Session["MyCart"];
			
			for (int i = 0; i < L.Count; i++)
			{
				OrderDetail od = new OrderDetail();
				od.Order_FID = o.Order_ID;
				od.Product_FID = L[i].Product_ID;
				od.Purchase_Price = L[i].Product_PurchasePrice;
				od.Quantity = L[i].Pro_Quantity;
				od.Sale_Price = L[i].Product_SalePrice;
				db.OrderDetails.Add(od);
				db.SaveChanges();

			}
			return RedirectToAction("OrderBooked");
        }
		public ActionResult OrderBooked()
        {
			Order o = (Order)Session["Order"];
			var s=db.Orders.Where(x => x.Order_ID == o.Order_ID).ToList();
			Session["MyCart"] = null;
			Session["count"] = null;
			Session["TotalAmount"] = null;
			return View(s);
        }

    }
}