using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Microsoft.Extensions.Logging;
using ReviewApp.WebApp.Controllers;

namespace TestProject1
{
    public class TestModelHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
          
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, results, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return results;
        }
        [Fact]
        public void Validate_UserAccountInput()
        {
            {
                var regUser = new ReviewApp.WebApp.Models.CustomerAccount
                {
                    FirstName = "UnitTest",
                    LastName = "test",
                    UserName = "unittest",
                    Email = "unittest@gmail.com",
                    Password = "dutran@123"

                };
                //var model = new ReviewApp.WebApp.Models.CustomerAccount();
                var validationResults = new List<ValidationResult>();
                //var results = TestModelHelper.Validate(regUser);
                var actual = Validator.TryValidateObject(regUser, new ValidationContext(regUser), validationResults, true);
                Assert.True(actual, "Expected to return true");
                //Assert.Equal("The Last Name field is required.", results[0].ErrorMessage);
            }
        }

        [Fact]
        public void Validate_CreateARestaurantInput()
        {
            {
                var Restaurant = new ReviewApp.Domain.Restaurant
                {
                    Name = "JimmyJohn",
                    Location = "1234 Walker Avenue, Grand Rapids",
                    Zipcode = "49544",
                    Contact = "(616)808-3456"

                };

                var validationResults = new List<ValidationResult>();
                var actual = Validator.TryValidateObject(Restaurant, new ValidationContext(Restaurant), validationResults, true);
                Assert.True(actual, "Expected to return true");

            }


        }
    }
}
