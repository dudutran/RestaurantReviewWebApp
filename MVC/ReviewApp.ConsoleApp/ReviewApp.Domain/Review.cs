using System;

namespace ReviewApp.Domain
{
    public class Review
    {
        public Review() { }
        public Review(int id, string comment, decimal rating, DateTime time)
        {
            this.Id = id;
            this.Comment = comment;
            this.Rating = rating;
            this.Time = time;
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
        public decimal Rating { get; set; }
    }
}
