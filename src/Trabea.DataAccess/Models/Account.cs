using System;
using System.Collections.Generic;

namespace Trabea.DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            PartTimeEmployees = new HashSet<PartTimeEmployee>();
            Roles = new HashSet<Role>();
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Employee? Employee { get; set; }
        public virtual ICollection<PartTimeEmployee> PartTimeEmployees { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
