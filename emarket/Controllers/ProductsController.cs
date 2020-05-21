using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emarket.Context;
using emarket.DbContext;
using System.IO;
using PagedList;

namespace emarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext db = new ProductContext();

        // GET: Products
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var products = from s in db.Product select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Category.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                //case "name_desc":
                //    products = products.OrderByDescending(s => s.Name);
                //    break;
                //case "Date":
                //    products = products.OrderBy(s => s.Price);
                //    break;
                //case "date_desc":
                //    products = products.OrderByDescending(s => s.Id);
                //    break;
                default:  // Name ascending 
                    products = products.OrderByDescending(s => s.Id);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var stream = (from m in db.Product where m.Id == id select m.Image).FirstOrDefault();
            //return File(stream, "image/jpeg");
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,File,Description,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Image");
                string fileName = Path.GetFileName(product.File.FileName);
                string fullPath = Path.Combine(path, fileName);
                product.File.SaveAs(fullPath);

                byte[] data;
                using (var inputStream = product.File.InputStream)
                {
                    var memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }

                if (data.Length > 0)
                    product.Image = data;
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name",product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,File,Description,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Image");
                string fileName = Path.GetFileName(product.File.FileName);
                string fullPath = Path.Combine(path, fileName);
                product.File.SaveAs(fullPath);

                byte[] data;
                using (var inputStream = product.File.InputStream)
                {
                    var memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                if (data.Length > 0)
                    product.Image = data;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
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
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
