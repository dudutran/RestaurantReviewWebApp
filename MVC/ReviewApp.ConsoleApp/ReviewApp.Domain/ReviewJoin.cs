using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp.Domain
{
    public class ReviewJoin
    {
        public ReviewJoin(int restaurantId, int customerId, int reviewId)
        {
            this.RestaurantId = restaurantId;
            this.CustomerId = customerId;
            this.ReviewId = reviewId;
        }
        public ReviewJoin(int id, int restaurantId, int customerId, int reviewId) : this(restaurantId, customerId, reviewId)
        {
            this.Id = id;
        }
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public int ReviewId { get; set; }
    }
}
