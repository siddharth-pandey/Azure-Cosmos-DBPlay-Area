using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AzureCosmosPlayAreaDocumentDb.Models;
using AzureCosmosPlayAreaDocumentDb.Persistence;

namespace AzureCosmosPlayAreaDocumentDb.Controllers
{
    public class CoursesController : Controller
    {
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await DocumentDbRepository<Course>.GetItemsAsync(d => d != null);

            return View(items);
        }

        public ActionResult Create()
        {
            return View(new Course());
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Course course)
        {
            if (ModelState.IsValid)
            {
                if (course.Id == null)
                {
                    await DocumentDbRepository<Course>.CreateItemAsync(course);
                }
                else
                {
                    await DocumentDbRepository<Course>.UpdateItemAsync(course.Id, course);
                }

                return RedirectToAction("Index");
            }

            return View(course);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = await DocumentDbRepository<Course>.GetItemAsync(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View("Create", course);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = await DocumentDbRepository<Course>.GetItemAsync(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind(Include = "Id")] string id)
        {
            await DocumentDbRepository<Course>.DeleteItemAsync(id);

            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            Course course = await DocumentDbRepository<Course>.GetItemAsync(id);

            return View(course);
        }
    }
}