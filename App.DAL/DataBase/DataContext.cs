using App.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.DataBase
{
	public class DataContext :DbContext
	{

		public DataContext(DbContextOptions<DataContext> options) : base(options) 
		{
		
		}
		
		public DbSet<User> Users { get; set; }
		public DbSet<UserToken> UserTokens { get; set; }
	}
}
