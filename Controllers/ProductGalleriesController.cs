using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multilinks.Models;

namespace Multilinks.Controllers
{
    public class ProductGalleriesController : Controller
    {
        private Model1 db = new Model1();

        // GET: ProductGalleries
        public ActionResult Index()
        {
            var productGalleries = db.ProductGalleries.Include(p => p.Product);
            return View(productGalleries.ToList());
        }

        // GET: ProductGalleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            if (productGallery == null)
            {
                return HttpNotFound();
            }
            return View(productGallery);
        }

        // GET: ProductGalleries/Create
        public ActionResult Create()
        {
            ViewBag.Product_FID = new SelectList(db.Products, "Product_ID", "Product_Name");
            return View();
        }

        // POST: ProductGalleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Gallery_ID,Gallery_URL,Product_FID")] ProductGallery productGallery)
        {
            if (ModelState.IsValid)
            {
                db.ProductGalleries.Add(productGallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_FID = new SelectList(db.Products, "Product_ID", "Product_Name", productGallery.Product_FID);
            return View(productGallery);
        }

        // GET: ProductGalleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            if (productGallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_FID = new SelectList(db.Products, "Product_ID", "Product_Name", productGallery.Product_FID);
            return View(productGallery);
        }

        // POST: ProductGalleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Gallery_ID,Gallery_URL,Product_FID")] ProductGallery productGallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productGallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_FID = new SelectList(db.Products, "Product_ID", "Product_Name", productGallery.Product_FID);
            return View(productGallery);
        }

        // GET: ProductGalleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            if (productGallery == null)
            {
                return HttpNotFound();
            }
            return View(productGallery);
        }

        // POST: ProductGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            db.ProductGalleries.Remove(productGallery);
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
