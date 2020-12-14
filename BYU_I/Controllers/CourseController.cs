﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BYU_I.DAL;
using BYU_I.Models;
using BYU_I.ViewModels;

namespace BYU_I.Controllers
{
	public class CourseController : Controller
	{
		private SchoolContext db = new SchoolContext();

		// GET: Course
		public ActionResult Index()
		{
			var courses = db.Courses.Include(c => c.Department);
			return View(courses.ToList());
		}

		// GET: Course/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Course course = db.Courses.Find(id);

			if (course == null)
			{
				return HttpNotFound();
			}

			return View(course);
		}

		// GET: Course/Create
		public ActionResult Create()
		{
			PopulateDepartmentsDropDownList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateCourseViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Course course = new Course
					{
						CourseID = model.CourseID,
						Title = model.Title,
						Credits = model.Credits,
						DepartmentID = model.DepartmentID
					};

					db.Courses.Add(course);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (RetryLimitExceededException /* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.)
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
			}

			PopulateDepartmentsDropDownList(model.DepartmentID);
			return View(model);
		}

		// GET: Course/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Course course = db.Courses.Find(id);

			if (course == null)
			{
				return HttpNotFound();
			}

			EditCourseViewModel model = new EditCourseViewModel
			{
				CourseID = course.CourseID,
				Title = course.Title,
				Credits = course.Credits,
				DepartmentID = course.DepartmentID
			};

			PopulateDepartmentsDropDownList(model.DepartmentID);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditCourseViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Course course = db.Courses.Find(model.CourseID);

					if (course == null)
					{
						return HttpNotFound();
					}

					course.Title = model.Title;
					course.Credits = model.Credits;
					course.DepartmentID = model.DepartmentID;

					db.Entry(course).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (RetryLimitExceededException /* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
			}

			PopulateDepartmentsDropDownList(model.DepartmentID);
			return View(model);
		}

		// GET: Course/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Course course = db.Courses.Find(id);
			if (course == null)
			{
				return HttpNotFound();
			}

			return View(course);
		}

		// POST: Course/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Course course = db.Courses.Find(id);
			db.Courses.Remove(course);
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

		private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
		{
			var departmentsQuery = from d in db.Departments
								   orderby d.Name
								   select d;
			ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
		}
	}
}
