using IC09_Ch11_SportsPro_rev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IC09_Ch11_SportsPro_rev.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext context { get; set; }
        public IncidentController(SportsProContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        [Route("[controller]s/{filter?}")]
        public IActionResult List(string filter = "all")
        {

            IQueryable<Incident> query = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .OrderBy(i => i.DateOpened);

            if (filter == "unassigned")
            {
                query = query.Where(i => i.TechnicianID == -1);
            }
            if (filter == "open")
            {
                query = query.Where(i => i.DateClosed == null);
            }

            var model = new IncidentListViewModel
            {
                Filter = filter,
                Incidents = query.ToList()
            };
            return View(model);
        }
        public IActionResult Filter(string id)
        {
            return RedirectToAction("List", new { Filter = id });
        }

        // New method to use the View Model
        private IncidentViewModel GetViewModel(string action = "")
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Customers = context.Customers
                    .OrderBy(c => c.FirstName)
                    .ToList(),
                Products = context.Products
                    .OrderBy(c => c.Name)
                    .ToList(),
                Technicians = context.Technicians
                    .OrderBy(c => c.Name)
                    .ToList(),
            };
            if (!String.IsNullOrEmpty(action))
                model.Action = action;

            return model;
        }

        [HttpGet]
        public IActionResult Add() // Modify this code
        {
            IncidentViewModel model = GetViewModel("Add");
            model.Incident = new Incident();

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IncidentViewModel model = GetViewModel("Edit");
            model.Incident = context.Incidents.Find(id)!;

            return View("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Save(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                {
                    context.Incidents.Add(incident);
                }
                else
                {
                    context.Incidents.Update(incident);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                // Add this code below
                var model = GetViewModel();
                model.Incident = incident;

                if (incident.IncidentID == 0)
                {
                    model.Action = "Add";
                }
                else
                {
                    model.Action = "Edit";
                }
                return View("AddEdit", model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = context.Incidents.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}