using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AzureCosmosPlayAreaMongoDb.Models;
using AzureCosmosPlayAreaMongoDb.Persistence;
using MongoDB.Driver;

namespace AzureCosmosPlayAreaMongoDb.Controllers
{
    public class CoursesController : Controller
    {
        private CoursesRepository _coursesRepository;

        public CoursesController()
        {
            var dbContext = new DbContext();
            _coursesRepository = new CoursesRepository(dbContext);
        }
        [ActionName("Index")]
        public ActionResult Index()
        {
            var courses = _coursesRepository.GetCourses();

            return View(courses);
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
                if (course.Id == default(Guid))
                {
                    await _coursesRepository.CreateCourseAsync(course);
                }
                else
                {
                    await _coursesRepository.UpdateCourseAsync(course.Id, course);
                }

                return RedirectToAction("Index");
            }

            return View(course);
        }

        [ActionName("Edit")]
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = _coursesRepository.GetCourse(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View("Create", course);
        }

        [ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = _coursesRepository.GetCourse(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind(Include = "Id")] Guid id)
        {
            await _coursesRepository.DeleteCourseAsync(id);

            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public ActionResult Details(Guid id)
        {
            Course course = _coursesRepository.GetCourse(id);

            return View(course);
        }
    }

}