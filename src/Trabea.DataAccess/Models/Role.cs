using System;
using System.Collections.Generic;

namespace Trabea.DataAccess.Models
{
    public partial class Role
    {
        public Role()
        {
            AccountEmails = new HashSet<Account>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Account> AccountEmails { get; set; }
    }
}
