using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp.Domain
{
        public interface IReviewRepo
        {
            Review AddReview(Review review);
            ReviewJoin AddAReviewJoin(ReviewJoin reviewjoin);
            Customer AddAUser(Customer customer);

            Restaurant AddARestaurant(Restaurant restaurant);
            List<Review> GetReviews();
            List<Restaurant> GetAllRestaurants();
            List<Customer> GetAllCustomers();
            List<ReviewJoin> GetReviewJoins();

            Restaurant FindARestaurantByZipcode(string zipcode);
            Restaurant FindARestaurant(string name);
            Restaurant FindARestaurant(int id);
            Customer SearchUsersByUserName(string userName);
            Customer SearchUsersById(int id);
            Review SearchReviewByReviewId(int id);
            
            
            void DeleteReviewJoin(int id);
            void DeleteReview(int id);
            void DeleteRestaurant(int id);
            void DeleteUser(int id);

            void Update(string email, int id);
            void UpdateRestaurant(string otherLocation, string otherContact, int id);
    }
        
}
