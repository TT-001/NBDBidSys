using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class InvType
    {
        [Key]
        public int itID { get; set; }

        [Display(Name = "Item Type")]
        [Required(ErrorMessage = "Item Type is required")]
        [StringLength(50, ErrorMessage = "Item size cannot be more than 50 characters long.")]
        public string itType { get; set; }

        public ICollection<Inventory> Inventories { get; set; }

    }
}
