﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.DAL.Helper.RegularExpression;

namespace App.DAL.Requests
{
	public class UserRegisterRequest
	{
		[Required]
		public string? FirstName { get; set; }
		[Required]
		public string? LastName { get; set; }
		[Required]
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Mobile { get; set; }
		public string? Role { get; set; }
	}
}