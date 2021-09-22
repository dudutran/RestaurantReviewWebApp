using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ReviewApp.WebApp.Models
{
    public class CustomerAccount
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", ErrorMessage = "Please enter the correct email!")]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Password length should be more than 5.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[^\da-zA-Z]).{6,20}$", ErrorMessage = "Password must be between 6 and 20 characters one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }
        
    }
    
}
