﻿using IC09_Ch11_SportsPro_rev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace IC09_Ch11_SportsPro_rev.Controllers
{
    public class ProductController : Controller
    {
        private SportsProContext context { get; set; }
        public ProductController(SportsProContext ctx) => context = ctx;

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List()
        {
            var products = context.Products.OrderBy(p => p.ReleaseDate).ToList();
            return View(products);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = context.Products.Find(id);
            return View("AddEdit", product);
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    context.Products.Add(product);
                    message = product.Name + " was added.";
                }
                else
                {
                    context.Products.Update(product);
                    message = product.Name + " was updated.";
                }
                context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                if (product.ProductID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            string message = string.Empty;
            message = product.Name + " was deleted.";
            context.Products.Remove(product);
            context.SaveChanges();
            TempData["message"] = message;
            return RedirectToAction("List");
        }
    }
}