using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class Project : IValidatableObject
    {
        public Project()
        {
            Bids = new HashSet<Bid>();
        }

        [Key]
        public int projID { get; set; }

        [Display(Name = "Begin Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime projBeginDate { get; set; }

        [Display(Name = "Estimated Completion Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? projCompletionDate { get; set; }

        [Display(Name = "Project Site")]
        [Required(ErrorMessage = "Project site is required")]
        [StringLength(200, ErrorMessage = "Project site cannot be more than 120 characters long.")]
        public string projSite { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        [RegularExpression(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$", ErrorMessage = "City not valid.")]
        [StringLength(50, ErrorMessage = "Project city cannot be more than 50 characters long.")]
        public string projCity { get; set; }

        public Province Province { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression("(^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$)", ErrorMessage = "Zip code is invalid.")] // US or Canada
        [Required(ErrorMessage = "Zip Code is Required.")]
        public String ZipCode { set; get; }


        public int? cliID { get; set; }
        public Client Client { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }//Added for concurrency

        public ICollection<Bid> Bids { get; set; }


        public string ProjSiteComplete
        {
            get
            {
                return $"{projSite}, {projCity}, {Province}, {ZipCode}";
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (projBeginDate >= projCompletionDate)
            {
                yield return new ValidationResult("Date of completion needs to be later than Date of beginning.", new[] { "projCompletionDate" });
            }
            if (projCompletionDate > DateTime.Today.AddDays(1))
            {
                yield return new ValidationResult("Date of completion cannot be in the future.", new[] { "projCompletionDate" });
            }
        }
    }
}
