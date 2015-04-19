using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
namespace MVC5Course.Controllers
{
    public class CRUDController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: CRUD
        public ActionResult Index()
        {
            var data = db.Product.
                Where(o => o.ProductName.StartsWith("C"));
            //var data = db.Database.SqlQuery<Product>("select * from product where productname like @p0", "C%").AsQueryable();
            return View(data);
        }

        // GET: CRUD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CRUD/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Product data = new Product()
                {
                    ProductName = collection["ProductName"],
                    Price = Convert.ToDecimal(collection["Price"]),
                    Active = true,
                    Stock = Convert.ToDecimal(collection["Stock"])
                };
                db.Product.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult BatchUpdate()
        {
            var data = db.Product.
                 Where(o => o.ProductName.StartsWith("C"));
            foreach (var innerData in data)
            {
                innerData.Price = innerData.Price * 2;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: CRUD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CRUD/Delete/5
        public ActionResult Delete(int id)
        {
            foreach (var orderLines in db.Client.Find(id).Order.Select(o => o.OrderLine))
            {
                db.OrderLine.RemoveRange(orderLines);
            }
            //db.OrderLine.RemoveRange(db.Client.Find(id).Order.Select(o => o.OrderLine).FirstOrDefault());
            db.Order.RemoveRange(db.Client.Find(id).Order);
            db.Client.Remove(db.Client.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: CRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult QueryProduct()
        {
            var data= db.QueryProduct().AsQueryable();

            return View(data);
        }
    }
}
