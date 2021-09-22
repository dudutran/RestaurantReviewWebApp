using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ReviewApp.Domain
{
    public class Customer
    {
        public Customer() { }
        public Customer(string firstName, string lastName, string username, string email, string password)
        {
            
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = username;
            this.Email = email;
            this.Password = password;
        }

        public Customer(int id, string firstName, string lastName, string username, string email, string password) /*: this(firstName, lastName, username, password, email)*/
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = username;
            this.Email = email;
            this.Password = password;
        }
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }


        public string UserName { get; set; }
        public string Password { get; set; }

        
    }

    
}
