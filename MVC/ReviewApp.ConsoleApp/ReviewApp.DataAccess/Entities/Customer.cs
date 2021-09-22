using System;
using System.Collections.Generic;

#nullable disable

namespace ReviewApp.DataAccess.Entities
{
    public partial class Customer
    {

        public Customer()
        {
            ReviewJoins = new HashSet<ReviewJoin>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ReviewJoin> ReviewJoins { get; set; }
    }
}
