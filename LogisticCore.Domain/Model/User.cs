using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Domain.Model
{

    [Table("User")]
    public class User:IDEntity
    {
      
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }    
        public int RoleId {  get; set; }
        public DateTime? Dob { get; set; }

        public string? RememberToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? loggedInAt { get; set; }

    }
}
