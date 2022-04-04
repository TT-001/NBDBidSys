using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class Labour
    {
        public Labour()
        {
            this.BidLabours = new HashSet<BidLabour>();
        }

        [Key]
        public int labID { get; set; }

        [Display(Name = "Description of Labour")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be more than 500 characters long.")]
        public string labDescription { get; set; }

        [Display(Name = "Labour Price")]
        [Required(ErrorMessage = "Labour Price is required")]
        [DataType(DataType.Currency)]
        public double labPrice { get; set; }

        public ICollection<BidLabour> BidLabours { get; set; }
    }
}
