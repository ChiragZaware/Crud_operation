using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application_crud.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;
using System.Net;

namespace Application_crud.Controllers
{
    public class CategoryController : Controller
    {
        ServicesContext db = new ServicesContext();

        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var products = db.Category.OrderBy(x => x.CategoryId).ToPagedList(pageNumber, pageSize);
            return View(products);


        }

        public ActionResult Create()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]


        public ActionResult Create(Class1 e)
        {

            if (ModelState.IsValid == true)
            {
                db.Category.Add(e);

                int a = db.SaveChanges();
                if (a > 0)
                {
                    ViewBag.CreateMessage = ("<script>alert('Data saved...')</script>");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CreateMessage = ("<script>alert('Data not saved...')</script>");
                }
            }
            

            return View();
        }
        public ActionResult Edit(int id)
        {
            var row = db.Category.Where(model => model.CategoryId == id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(Class1 e)
        {
            db.Entry(e).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a > 0)
            {
               
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.UpdateMessage = ("<script>alert('Data not Modified...')</script>");
            }
           
            db.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var row = db.Category.Where(model => model.CategoryId == id).FirstOrDefault();
            return View(row);
        }


        public ActionResult Delete(Class1 e)
        {
            db.Entry(e).State = EntityState.Deleted;
            int a = db.SaveChanges();

            if (a > 0)
            {
                ViewBag.DeleteMessage = ("<script>alert('Data delete succesfully...')</script>");
                ModelState.Clear();
               
            }
            else
            {
                ViewBag.DeleteMessage = ("<script>alert('Data not delete succesfully...')</script>");
            }

            db.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class1 product = db.Category.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}