using System;
using System.Collections.Generic;

#nullable disable

namespace ReviewApp.DataAccess.Entities
{
    public partial class ReviewJoin
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Review Review { get; set; }
    }
}
