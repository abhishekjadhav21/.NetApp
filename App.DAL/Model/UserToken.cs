using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.DAL.Model
{
	public class UserToken
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TokenId { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public string Token { get; set; } = string.Empty;
		[Required]
		public DateTime? ExpiredDate { get; set; } = DateTime.UtcNow;
		[Required]
		public string? Role { get; set; }

		[JsonIgnore]
		public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

		[JsonIgnore]
		public DateTime? LastModifiedDate { get; set; } = DateTime.UtcNow;
	}
}
