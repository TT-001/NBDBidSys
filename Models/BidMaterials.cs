using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class BidMaterial
    {
        public int invID { get; set; }
        public Inventory Inventory { get; set; }
        public int bidID { get; set; }
        public Bid Bid { get; set; }

        [Display(Name = "Quantity of Materials Needed")]
        [Required(ErrorMessage = "Quantity of Materials Needed is required")]
        public int bmQuantity { get; set; }

        [Display(Name = "Total Cost")]
        [Required(ErrorMessage = "Total Cost is required")]
        [DataType(DataType.Currency)]
        public double bmCost { get; set; }
    }
}
