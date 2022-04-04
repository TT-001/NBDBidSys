using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class Inventory
    {
        public Inventory()
        {
            this.BidMaterials = new HashSet<BidMaterial>();
        }
        [Key]
        public int invID { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(50, ErrorMessage = "Item name cannot be more than 50 characters long.")]
        public string invName { get; set; }

        [Display(Name = "Item Size")]
        [Required(ErrorMessage = "Item Size is required")]
        [StringLength(50, ErrorMessage = "Item size cannot be more than 50 characters long.")]
        public string invSize { get; set; }

        [Display(Name = "Item Price")]
        [Required(ErrorMessage = "Item Price is required")]
        [DataType(DataType.Currency)]
        public double invPrice { get; set; }

        public int? itID { get; set; }
        public InvType InvType { get; set; }

        public ICollection<BidMaterial> BidMaterials { get; set; }
    }
}
