﻿using IC09_Ch11_SportsPro_rev.Models;
using Microsoft.AspNetCore.Mvc;

namespace IC09_Ch11_SportsPro_rev.Controllers
{
    public class TechnicianController : Controller
    {
        private SportsProContext context { get; set; }
        public TechnicianController(SportsProContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        public IActionResult List()
        {
            var techs = context.Technicians
                .Where(t => t.TechnicianID > -1)  // don't include default value for unassigned
                .OrderBy(t => t.Name)
                .ToList();
            return View(techs);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Technician());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var tech = context.Technicians.Find(id);
            return View("AddEdit", tech);
        }

        [HttpPost]
        public IActionResult Save(Technician tech)
        {
            if (ModelState.IsValid)
            {
                if (tech.TechnicianID == 0)
                {
                    context.Technicians.Add(tech);
                }
                else
                {
                    context.Technicians.Update(tech);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                if (tech.TechnicianID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View(tech);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var tech = context.Technicians.Find(id);
            return View(tech);
        }

        [HttpPost]
        public IActionResult Delete(Technician tech)
        {
            context.Technicians.Remove(tech);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}