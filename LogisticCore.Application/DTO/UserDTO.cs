using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public DateTime? Dob { get; set; }

        public string RememberToken { get; set; } = string.Empty;
       
        public DateTime? CreatedAt { get; set; }
        public DateTime? loggedInAt { get; set; }
        public string OldPassword { get; set; } = string.Empty;
    }
}
