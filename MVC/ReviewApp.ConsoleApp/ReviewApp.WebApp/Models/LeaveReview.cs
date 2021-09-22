using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewApp.WebApp.Models
{
    public class LeaveReview
    {
        [Required]
        public string Comment { get; set; }
        public DateTime Time { get; set; }

        [Required]
        [Range(0, 5)]
        public decimal Rating { get; set; }
    }
}
