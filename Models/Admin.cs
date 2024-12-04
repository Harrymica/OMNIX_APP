using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNIX_App.Models
{
    [Table("admins")]
    public class Admin : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }
        [Column("full_name")]
        public string? FullName { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
        [Column("role")]
        public string? Role { get; set; }
    }

}
