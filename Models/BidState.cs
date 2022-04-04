using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    //public class BidState
    //{
    //    public BidState()
    //    {
    //        Bids = new HashSet<Bid>();
    //    }
    //    [Key]
    //    public int bidStateID { get; set; }

    //    [Display(Name = "Bid Status")]
    //    [Required(ErrorMessage = "Bid Status is required")]
    //    [StringLength(15, ErrorMessage = "Bid Status is no more than 15 characters")]
    //    public string status { get; set; }

    //    public IEnumerable<Bid> Bids { get; set; }
    //}

    //For Designer
    public enum BidState
    {
        [Display(Name = "Design Stage")] Designing = 1,
        [Display(Name = "Waiting Management Approval")] Waiting_Management,
        [Display(Name = "Management Approved, Waiting Client Approval")] bsClientApproval,
        [Display(Name = "Needs Revision")] bsRevision,
        [Display(Name = "Approved")] Approved,

    }
}
