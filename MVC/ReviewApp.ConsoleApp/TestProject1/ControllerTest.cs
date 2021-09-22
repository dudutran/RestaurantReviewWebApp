using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Microsoft.Extensions.Logging;
using ReviewApp.WebApp.Controllers;
using Moq;
using ReviewApp.DataAccess;
using ReviewApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace TestProject1
{
    public class ControllerTest
    {
        [Fact]
        public void ProveThatUserControllerIsCalled()
        {
            var logger = new Mock<ILogger<UserController>>();
            var mockRepo = new Mock<IReviewRepo>();

            var userController = new UserController(mockRepo.Object, logger.Object);

            var result = userController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewResult, result);

        }

        [Fact]
        public void ProveThatRestaurantControllerIsCalled()
        {
            var logger = new Mock<ILogger<RestaurantController>>();
            var mockRepo = new Mock<IReviewRepo>();

            var restaurantController = new RestaurantController(mockRepo.Object, logger.Object);

            var result = restaurantController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewResult, result);

        }
    }
}
