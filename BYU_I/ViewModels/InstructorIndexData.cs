using BYU_I.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BYU_I.ViewModels
{
	public class InstructorIndexData
	{
		public IEnumerable<Instructor> Instructors { get; set; }
		public IEnumerable<Course> Courses { get; set; }
		public IEnumerable<Enrolment> Enrolments { get; set; }
	}
}