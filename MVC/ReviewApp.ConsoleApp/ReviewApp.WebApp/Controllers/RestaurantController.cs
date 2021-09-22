using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewApp.Domain;
using ReviewApp.DataAccess;
using ReviewApp.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ReviewApp.WebApp.Controllers
{
    
    public class RestaurantController : Controller
    {
        private readonly IReviewRepo _repo;
        private readonly ILogger<RestaurantController> _logger;
        public RestaurantController(IReviewRepo repo, ILogger<RestaurantController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public static int restaurantid;
        //Get all list of restaurants
        public IActionResult Index()
        {
            List<ReviewApp.Domain.Restaurant> restaurants = _repo.GetAllRestaurants();

            return View(restaurants);
        }

        
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult CreateARestaurant()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateARestaurant(ReviewApp.Domain.Restaurant restaurant)
        {
            try
            {
                _repo.AddARestaurant(restaurant);
            }
            catch(Exception e)
            {
                
                ModelState.AddModelError("Location", e.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        //Search for a restaurant
        public IActionResult SearchForARestaurantForm()
        {
            return View();
        }
        //Show the search results
        public IActionResult ShowSearchResults(string searchString)
        {
             if (searchString == null)
             {
                 return NotFound();
             }
 
             var foundRestaurant = _repo.FindARestaurant(searchString);

            //add all found restaurants to List<>
            List<ReviewApp.Domain.Restaurant> restaurants = new List<ReviewApp.Domain.Restaurant>();
             
            if (foundRestaurant != null)
             restaurants.Add(foundRestaurant);

             return View("Index", restaurants);
        }

 

        //Get restaurant details
        public IActionResult Details2(int? id)
        {
            var restaurant = _repo.GetAllRestaurants().First(x => x.Id == id);
            
            return View(restaurant.Name);
        }
        public IActionResult Details(int id)
        {
            List<ReviewApp.Domain.ReviewJoin> reviewjoins = _repo.GetReviewJoins();
            var restaurant = _repo.GetAllRestaurants().First(x => x.Id == id);

            RestaurantController.restaurantid = restaurant.Id; //store this data in case the user leave a review for this restaurant

            decimal averageRating = AverageRating(restaurant.Name);
            TempData["averageRating"] = Convert.ToString(averageRating);
            TempData["restaurant_name"] = restaurant.Name;

            //get all reviews belonging to the restaurant
            List<ReviewApp.Domain.Review> reviews = new List<ReviewApp.Domain.Review>();
            for (int i = 0; i < reviewjoins.Count; i++)
            {
                if (restaurant.Id==reviewjoins[i].RestaurantId)
                {
                    var foundReview = _repo.SearchReviewByReviewId(reviewjoins[i].ReviewId);
                    reviews.Add(foundReview);
                }
                //return View(foundReview);
            }
            return View(reviews);
        }

        //Average rating
        public decimal AverageRating(string restaurantname)
        {
            decimal averageRating;
            decimal sum = 0;
            int n = 0;
            var foundRestaurant = _repo.FindARestaurant(restaurantname);
            List<ReviewApp.Domain.ReviewJoin> reviewjoins = _repo.GetReviewJoins();
            for (int i = 0; i < reviewjoins.Count; i++)
            {
                if (reviewjoins[i].RestaurantId == foundRestaurant.Id)
                {
                    Domain.Review foundReview = _repo.SearchReviewByReviewId(reviewjoins[i].ReviewId);
                    sum += foundReview.Rating;
                    n += 1;
                }
            }
            if (n == 0) { averageRating = 0; }
            else { averageRating = Math.Round(sum / n, 2); }
            return averageRating;
        }

        //Leave reviews
        public IActionResult Review(int id)
        {
            return View(_repo.GetReviews().First(x => x.Id == id));
        }

        [HttpGet]
        [Authorize]
        public IActionResult LeaveReviews()
        {
 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LeaveReviews(ReviewApp.Domain.Review review)
        {
            

            if(!ModelState.IsValid)
            {
                return View();
            }
            
            int customerId = UserController.userid;//CustomerId??
            int restaurantId = RestaurantController.restaurantid;//RestaurantId??
            //If customerId = 0 that means they were logged out, please log in again
            try
            {
                if (customerId == 0)
                {
                    throw new Exception("To leave Review, customerId cannot be 0");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "This exception from Leave Review");
            }

            if (customerId == 0) return View("ErrorMessage", model: "Please logout and log in again!");
            _repo.AddReview(review);
            List<ReviewApp.Domain.Review> reviews = _repo.GetReviews();
            int id = reviews[reviews.Count - 1].Id;

            
            ReviewApp.Domain.ReviewJoin reviewjoin = new ReviewApp.Domain.ReviewJoin(restaurantId, customerId, id);
            _repo.AddAReviewJoin(reviewjoin);

            return RedirectToAction("Review", new { id });
        }

        //Update information of restaurants
        [HttpGet]
        public IActionResult UpdateRestaurant(int id)
        {
            var customer = _repo.GetAllRestaurants().First(x => x.Id == id);
            var restaurant = _repo.FindARestaurant(id);
            TempData["R_name"] = restaurant.Name;
            return View();
        }
        [HttpPost]
        public IActionResult UpdateRestaurant(string otherLocation, string otherContact, int id)
        {
            
            _repo.UpdateRestaurant(otherLocation, otherContact, id);
            return View(); 
        }


        // Delete a Restaurant
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var restaurant = _repo.GetAllRestaurants().First(x => x.Id == id);
            ReviewApp.Domain.Restaurant foundRestaurant = _repo.FindARestaurant(restaurant.Name);
            return View(foundRestaurant);
        }
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            _logger.LogInformation("Admin get access to the page");

            var restaurant = _repo.GetAllRestaurants().First(x => x.Id == id);
            ReviewApp.Domain.Restaurant foundRestaurant = _repo.FindARestaurant(restaurant.Name);
            List<ReviewApp.Domain.ReviewJoin> reviewjoins = _repo.GetReviewJoins();
            for (int i = 0; i < reviewjoins.Count; i++)
            {
                if (reviewjoins[i].RestaurantId == foundRestaurant.Id)
                {
                    _repo.DeleteReviewJoin(reviewjoins[i].Id);
                    _repo.DeleteReview(reviewjoins[i].ReviewId);
                }
            }
            _repo.DeleteRestaurant(id);
            return RedirectToAction("ConfirmPage");
        }

        //Delete Review
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteReview(int id)
        {
            var review = _repo.GetReviews().First(x => x.Id == id);
            ReviewApp.Domain.Review foundReview = _repo.SearchReviewByReviewId(review.Id);
            return View(foundReview);
        }
        [HttpPost]
        public IActionResult DeleteReview(int id, IFormCollection collection)
        {
            _logger.LogInformation("Admin get access to the delete review page");

            //var restaurant = _repo.GetAllRestaurants().First(x => x.Id == id);
            //ReviewApp.Domain.Restaurant foundRestaurant = _repo.FindARestaurant(restaurant.Name);
            ReviewApp.Domain.Review foundReview = _repo.SearchReviewByReviewId(id);
            List<ReviewApp.Domain.ReviewJoin> reviewjoins = _repo.GetReviewJoins();
            for (int i = 0; i < reviewjoins.Count; i++)
            {
                if (reviewjoins[i].ReviewId == foundReview.Id)
                {
                    _repo.DeleteReviewJoin(reviewjoins[i].Id);   
                }
            }
            _repo.DeleteReview(id);
            return RedirectToAction("ConfirmPage");
        }

        public IActionResult ConfirmPage()
        {
            return View();
        }
    }
}
