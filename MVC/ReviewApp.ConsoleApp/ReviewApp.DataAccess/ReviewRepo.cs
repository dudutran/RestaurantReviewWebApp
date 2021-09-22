using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReviewApp.DataAccess;
using ReviewApp.DataAccess.Entities;
using ReviewApp.Domain;


namespace ReviewApp.DataAccess
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ReviewDbContext _context;
        public ReviewRepo(ReviewDbContext context)
        {
            _context = context;
        }

        public ReviewApp.Domain.Review AddReview(ReviewApp.Domain.Review review)
        {
            _context.Reviews.Add(
                new Entities.Review
                {
                    Comment = review.Comment,
                    Rating = (review.Rating),
                    Time = DateTime.Now
                }
            );
            _context.SaveChanges();

            return review;
        }
        public List<ReviewApp.Domain.Review> GetReviews()
        {
            return _context.Reviews.Select(
                review => new ReviewApp.Domain.Review(review.Id, review.Comment, (decimal)review.Rating, review.Time)
            ).ToList();
        }

        public ReviewApp.Domain.Restaurant FindARestaurant(string name)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Name == name);
            if (foundRestaurant != null)
            {
                return new ReviewApp.Domain.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Contact, foundRestaurant.Zipcode);
            }
            return new ReviewApp.Domain.Restaurant();
        }

        public ReviewApp.Domain.Restaurant FindARestaurant(int id)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Id == id);
            if (foundRestaurant != null)
            {
                return new ReviewApp.Domain.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Contact, foundRestaurant.Zipcode);
            }
            return new ReviewApp.Domain.Restaurant();
        }
        public ReviewApp.Domain.Restaurant FindARestaurantByZipcode(string zipcode)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Zipcode == zipcode);
            if (foundRestaurant != null)
            {
                return new ReviewApp.Domain.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Contact, foundRestaurant.Zipcode);
            }
            return new ReviewApp.Domain.Restaurant();
        }
        public List<ReviewApp.Domain.Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.Select(
                restaurant => new ReviewApp.Domain.Restaurant(restaurant.Id, restaurant.Name, restaurant.Location, restaurant.Contact, restaurant.Zipcode)
            ).ToList();
        }
        public List<ReviewApp.Domain.Customer> GetAllCustomers()
        {
            return _context.Customers.Select(
                customer => new ReviewApp.Domain.Customer(customer.Id, customer.FirstName, customer.LastName, customer.UserName, customer.Email, customer.Password)
            ).ToList();
        }
        public ReviewApp.Domain.Customer AddAUser(ReviewApp.Domain.Customer customer)
        {

            if (_context.Customers.Any(c => c.UserName == customer.UserName))
            {
                throw new InvalidOperationException("This username has been already used");
            }
            
            _context.Customers.Add(
                new Entities.Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    UserName = customer.UserName,
                    Email = customer.Email,
                    Password = customer.Password
                }
            ) ; 
            _context.SaveChanges();
            return customer;
        }

        public ReviewApp.Domain.Restaurant AddARestaurant(ReviewApp.Domain.Restaurant restaurant)
        {
            if (_context.Restaurants.Any(r => r.Location == restaurant.Location))
            {
                throw new Exception("This location has been already added");
            }
            _context.Restaurants.Add(
                new Entities.Restaurant
                {
                    Name = restaurant.Name,
                    Location = restaurant.Location,
                    Contact = restaurant.Contact,
                    Zipcode = restaurant.Zipcode
                }
            );
            _context.SaveChanges();
            return restaurant;
        }
            public ReviewApp.Domain.Customer SearchUsersByUserName(string userName)
        {
            Entities.Customer foundCustomer = _context.Customers
                .FirstOrDefault(customer => customer.UserName == userName);
            if (foundCustomer != null)
            {
                return new ReviewApp.Domain.Customer(foundCustomer.Id, foundCustomer.FirstName, foundCustomer.LastName, foundCustomer.UserName, foundCustomer.Email, foundCustomer.Password);
            }
            return new ReviewApp.Domain.Customer();
        }
        public ReviewApp.Domain.Customer SearchUsersById(int id)
        {
            Entities.Customer foundCustomer = _context.Customers
                .FirstOrDefault(customer => customer.Id == id);
            if (foundCustomer != null)
            {
                return new ReviewApp.Domain.Customer(foundCustomer.Id, foundCustomer.FirstName, foundCustomer.LastName, foundCustomer.UserName, foundCustomer.Email, foundCustomer.Password);
            }
            return new ReviewApp.Domain.Customer();
        }
        public void Update(string otherEmail, int id)
        {
            // query the DB
            var customer = _context.Customers.First(customer => customer.Id == id);

            customer.Email = otherEmail;

            // write changes to DB
            _context.SaveChanges();
        }
        public void UpdateRestaurant(string otherLocation, string otherContact, int id)
        {
            var restaurant = _context.Restaurants.First(restaurant => restaurant.Id == id);
            restaurant.Location = otherLocation;
            restaurant.Contact = otherContact;

            // write changes to DB
            _context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            Entities.Customer foundCustomer = _context.Customers
                .FirstOrDefault(customer => customer.Id == id);
            _context.Customers.Remove(foundCustomer);
            _context.SaveChanges();
        }
        public List<ReviewApp.Domain.ReviewJoin> GetReviewJoins()
        {
            return _context.ReviewJoins.Select(
                reviewjoin => new ReviewApp.Domain.ReviewJoin(reviewjoin.Id, reviewjoin.RestaurantId, reviewjoin.CustomerId, reviewjoin.ReviewId)
            ).ToList();
        }
        public ReviewApp.Domain.Review SearchReviewByReviewId(int id)
        {
            Entities.Review foundReview = _context.Reviews
                .FirstOrDefault(review => review.Id == id);
            if (foundReview != null)
            {
                return new ReviewApp.Domain.Review(foundReview.Id, foundReview.Comment, (decimal)foundReview.Rating, foundReview.Time);
            }
            return new ReviewApp.Domain.Review();
        }
        public ReviewApp.Domain.ReviewJoin AddAReviewJoin(ReviewApp.Domain.ReviewJoin reviewjoin)
        {
            _context.ReviewJoins.Add(
                new Entities.ReviewJoin
                {
                    RestaurantId = reviewjoin.RestaurantId,
                    CustomerId = reviewjoin.CustomerId,
                    ReviewId = reviewjoin.ReviewId
                }
            );
            _context.SaveChanges();
            return reviewjoin;
        }
        public void DeleteReviewJoin(int id)
        {
             Entities.ReviewJoin foundReviewJoin = _context.ReviewJoins
                .FirstOrDefault(reviewjoin => reviewjoin.Id == id);
            _context.ReviewJoins.Remove(foundReviewJoin);
            _context.SaveChanges();
        }
        public void DeleteReview(int id)
        {
            Entities.Review foundReview = _context.Reviews
                .FirstOrDefault(review => review.Id == id);
            _context.Reviews.Remove(foundReview);
            _context.SaveChanges();
        }
        public void DeleteRestaurant(int id)
        {
            
            Entities.Restaurant foundRestaurant = _context.Restaurants
                .FirstOrDefault(restaurant => restaurant.Id == id);
            _context.Restaurants.Remove(foundRestaurant);
            _context.SaveChanges();
        }
        


    }
}
