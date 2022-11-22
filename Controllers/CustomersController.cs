using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multilinks.Models;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using System.Text;
namespace Multilinks.Controllers
{
  
    public class CustomersController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult WebForm1()
        {
            return View();
        }

        // GET: Customers


        public ActionResult CustomerSignup()
        {
            if (TempData.ContainsKey("NotValid") && (bool)TempData["NotValid"])
            {
                ViewBag.Message = "Please Enter Valid Credentials";
            }
            return View();
        }
        public ActionResult Index()
        {
            return View(db.Customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_ID,Customer_Name,Customer_Email,Customer_Password,Customer_Contact,Customer_Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("CustomerSignup");
            }

            return View(customer);
        }
        [HttpPost]
        public ActionResult CustomerLogin(Customer c)
        {
            Customer a=db.Customer.Where(x =>x.Customer_Email == c.Customer_Email && x.Customer_Password == c.Customer_Password).FirstOrDefault();
            if (a!= null)
            {
                Session["Customer"] = a;
                return RedirectToAction("IndexCustomer", "Home");
            }
            else
            {
                TempData["NotValid"] = true;
                return RedirectToAction("CustomerSignup");
            }
        }
        public ActionResult CustomerLogout()
        {
            Session["Customer"] = null;
            return RedirectToAction("CustomerSignup");
        }
        public ActionResult RecentOrders()
        {
            Customer c = (Customer)Session["Customer"];
            var o = db.Orders.Where(x => x.Customer_FID == c.Customer_ID && x.Order_Status!="Cancelled").OrderByDescending(x=>x.Order_Date).ToList();
            return View(o);
        }
        public ActionResult CancelledOrders()
        {
            Customer c = (Customer)Session["Customer"];
            var o = db.Orders.Where(x => x.Customer_FID == c.Customer_ID && x.Order_Status=="Cancelled").ToList();
            return View(o);
        }
        public ActionResult Invoice(int id)
        {

            Customer c = (Customer)Session["Customer"];
            var s = db.Orders.Where(x => x.Order_ID == id && x.Customer_FID==c.Customer_ID).ToList();
            return View(s);
        }
        public ActionResult Cancel(int id)
        {
            Order o = db.Orders.Where(x => x.Order_ID == id).FirstOrDefault();
            o.Order_Status = "Cancelled";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CancelledOrders");
        }
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_ID,Customer_Name,Customer_Email,Customer_Password,Customer_Contact,Customer_Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
