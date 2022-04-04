using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class Bid
    {
        public Bid()
        {
            this.BidLabours = new HashSet<BidLabour>();
            this.BidMaterials = new HashSet<BidMaterial>();
        }
        [Key]
        public int bidID { get; set; }

        [Display(Name = "Bid Placed")]
        [Required(ErrorMessage = "Bid Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime bidDate { get; set; }

        [Display(Name = "Bid Amount")]
        [DataType(DataType.Currency)]
        public decimal bidAmount { get; set; }

        public int projID { get; set; }
        public Project Project { get; set; }

        [ForeignKey("SalesAssociate")]
        public int SalesID { get; set; }
        public Employee SalesAssociate { get; set; }

        [ForeignKey("Designer")]
        public int DesignerID { get; set; }
        public Employee Designer { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }//Added for concurrency

        //After BidState was made enum
        //public int bidStateID { get; set; }
        public BidState BidState { get; set; }

        public ICollection<BidLabour> BidLabours { get; set; }
        public ICollection<BidMaterial> BidMaterials { get; set; }

        public Employee employee { get; set; }

    }
}
