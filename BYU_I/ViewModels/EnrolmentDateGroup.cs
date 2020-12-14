using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BYU_I.ViewModels
{
	public class EnrolmentDateGroup
	{
		[DataType(DataType.Date)]
		public DateTime? EnrolmentDate { get; set; }

		public int StudentCount { get; set; }
	}
}