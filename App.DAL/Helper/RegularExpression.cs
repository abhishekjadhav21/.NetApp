using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Helper
{
	public class RegularExpression
	{
		public class EmailAddres : RegularExpressionAttribute
		{
			public EmailAddres() : base(@"^([a-z0-9\+_\-]+)(\.[a-z0-9\+_\-]+)*@([a-z0-9\-]+\.)+[a-z]{2,6}$")
			{
			}
		}

		//Date=yyyy/mm/dd or yyyy-mm-dd
		public class Date : RegularExpressionAttribute
		{
			public Date() : base(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$")
			{
			}
		}

		public class Name : RegularExpressionAttribute
		{
			public Name() : base("[a-zA-Z ]*$")
			{
			}
		}

		public class Password : RegularExpressionAttribute
		{
			public Password() : base("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$")
			{
			}
		}

		public class ZipCode : RegularExpressionAttribute
		{
			public ZipCode() : base("[0-9]{6}(?:-[0-9]{6})?$")
			{
			}
		}

		//without countrycode ex.9012345665
		public class MobileNo : RegularExpressionAttribute
		{
			public MobileNo() : base("^[0-9]{10}$")
			{
			}
		}

		//with countrycode ex.+918023234554
		public class Phonenumber : RegularExpressionAttribute
		{
			public Phonenumber() : base("^[+]{1}[0-9]{12}$")
			{
			}
		}

		//format yyyy-mm-dd hh:mm:ss or yyyy/mm/dd hh:mm:ss
		public class Date_Time : RegularExpressionAttribute
		{
			public Date_Time() : base(@"^([0-9]{4})[\-\/\s]?([0-1][0-9])[\-\/\s]?([0-3][0-9])\s([0-1][0-9]|[2][0-3]):([0-5][0-9]):([0-5][0-9])$")
			{
			}
		}

		//DateFormat dd/mm/yyyy .Ex 23/03/2023
		public class Date2 : RegularExpressionAttribute
		{
			public Date2() : base(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$")
			{
			}
		}

		//Format HH:MM AM or PM
		public class Time : RegularExpressionAttribute
		{
			public Time() : base(@"^(1[0-2]|0?[1-9]):[0-5][0-9] (AM|PM)$")
			{
			}
		}

		// Format mm/dd/yyyy hh:mm:ss
		public class DateTimeReg : RegularExpressionAttribute
		{
			public DateTimeReg() : base(@"^(0[1-9]|1[0-2])/(0[1-9]|1\d|2\d|3[01])/((19|20)\d{2})\s([01]\d|2[0-3]):([0-5]\d):([0-5]\d)$")
			{
			}
		}

		//format mm/dd/yyyy
		public class Date3 : RegularExpressionAttribute
		{
			public Date3() : base(@"^(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])/([0-9]{4})$")
			{
			}
		}

		//format HH:mm:ss
		public class Time1 : RegularExpressionAttribute
		{
			public Time1() : base(@"^([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$")
			{
			}
		}
	}
}
