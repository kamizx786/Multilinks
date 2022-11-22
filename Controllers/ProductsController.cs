using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multilinks.Models;

namespace Multilinks.Controllers
{
    public class ProductsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Category_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
         {
       
            if (ModelState.IsValid)
            {

                product.Pro_Pic.SaveAs(Server.MapPath("~/ProductPicture/"+product.Pro_Pic.FileName));
                product.Product_Picture = "~/ProductPicture/" +product.Pro_Pic.FileName;
                db.Products.Add(product);
                db.SaveChanges();
                //         ProductGallery productGallery = new ProductGallery();
                //         foreach(var file in product.GalleryPic)
                //           {
                //                string path = AppDomain.CurrentDomain.BaseDirectory + "~/GallerPics/";
                //               string filename = file.GetHashCode().ToString();
                //               string fullpath = Path.Combine(path,filename);
                //               productGallery.Gallery_URL = fullpath;
                //           productGallery.Product_FID = product.Product_ID;
                //            db.ProductGalleries.Add(productGallery);
                //             db.SaveChanges();
                //      }

                return RedirectToAction("Index");
            }

            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Category_Name", product.Category_FID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
       
            Product product = db.Products.Find(id);
            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Category_Name", product.Category_FID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
           
            if (ModelState.IsValid)
            {
                if (product.Pro_Pic != null)
                {
                    product.Pro_Pic.SaveAs(Server.MapPath("~/ProductPicture/" + product.Pro_Pic.FileName));
                    product.Product_Picture = "~/ProductPicture/" + product.Pro_Pic.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Category_Name", product.Category_FID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
