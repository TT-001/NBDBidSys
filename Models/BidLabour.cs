using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class BidLabour
    {
        public int labID { get; set; }
        public Labour Labour { get; set; }
        public int bidID { get; set; }
        public Bid Bid { get; set; }

        [Display(Name = "Hours of Work Needed")]
        [Required(ErrorMessage = "Hours of Work Needed is required")]
        public double blHours { get; set; }

        [Display(Name = "Total Cost")]
        [Required(ErrorMessage = "Total Cost is required")]
        [DataType(DataType.Currency)]
        public double blCost { get; set; }
    }
}
