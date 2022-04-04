using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class inventoryVM
    {
        [Key]
        public int ID { get; set; }

        [Display(Name ="Quantity")]
        public int quantityOnHands { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Price")]
        public decimal price { get; set; }
        [Display(Name = "Type of Material")]
        public string type { get; set; }
    }
}
