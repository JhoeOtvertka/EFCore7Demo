using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Category = DAL.Category;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private DatabaseContext db;

        public ProductController()
        {
            db = new DatabaseContext();
        }

        public IActionResult Index()
        {
            var products = (from prd in db.Products
                join cat in db.Categories
                    on prd.CategorId equals cat.CategoryId
                select new ProductViewModel
                {
                    ProductId = prd.ProductId,
                    Name = prd.Name,
                    UnitPrice = prd.UnitPrice,
                    Description = prd.Description,
                    Category = cat.Name

                }).ToList();

                 

            return View(products);
        }


        public IActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            try
            {
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = "Something went wrong!";
            }

            ViewBag.Categories = db.Categories.ToList();

            return View();
        }


        public IActionResult Edit(int Id)
        {
            ViewBag.Categories = db.Categories.ToList();
            Product prd = db.Products.Where(p => p.ProductId == Id).FirstOrDefault();

            return View("Create", prd);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            try
            {
                //Product prd = db.Products.Where(p => p.ProductId == model.ProductId).FirstOrDefault();
                //prd.UnitPrice = model.UnitPrice;

                db.Products.Update(model);

                //db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
            ViewBag.Categories = db.Categories.ToList();

            return View("Create");
        }


        public IActionResult Delete(int Id)
        {
   
            Product prd = db.Products.Where(p => p.ProductId == Id).FirstOrDefault();

            if (prd != null)
            {
                db.Products.Remove(prd);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}