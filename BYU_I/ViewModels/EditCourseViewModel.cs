﻿using BYU_I.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BYU_I.ViewModels
{
	public class EditCourseViewModel
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Display(Name = "Number")]
		public int CourseID { get; set; }

		[StringLength(50, MinimumLength = 3)]
		public string Title { get; set; }

		[Range(0, 5)]
		public int Credits { get; set; }

		public int DepartmentID { get; set; }

		public virtual Department Department { get; set; }
	}
}