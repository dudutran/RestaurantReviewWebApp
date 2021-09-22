using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewApp.Domain
{
    public class Restaurant
    {
        public Restaurant() { }

        public Restaurant(string name, string zipcode)
        {
            this.Name = name;
            this.Zipcode = zipcode;
        }

        public Restaurant(int id, string name, string location, string contact, string zipcode)
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
            this.Contact = contact;
            this.Zipcode = zipcode;
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(14)]
        public string Contact { get; set; }

        [MinLength(5)]
        [Required]
        public string Zipcode { get; set; }
  
    }
}
