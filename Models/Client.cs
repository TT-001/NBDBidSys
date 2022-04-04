using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{

    public enum Province
    {
        AB = 1, BC, MB, NB, NL, NS, ON, PE, QC, SK
    }

    //public struct Address
    //{
    //    //The address type can aslo be added to this struct if it is to be used for other type of entities.
    //    //public string type {get; set;}



    //    [Display(Name = "Street Address")]
    //    [Required(ErrorMessage = "Client street address is required")]
    //    [RegularExpression(@"^[a-zA-Z 0-9]+$", ErrorMessage = "Street Address not valid.")]
    //    [StringLength(100, ErrorMessage = "Client address cannot be more than 200 characters long.")]
    //    public string Street { get; set; }


    //    public Province Province { get; set; }

    //    [Display(Name = "Zip Code")]
    //    [StringLength(10, MinimumLength = 5)]
    //    [RegularExpression("(^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$)", ErrorMessage = "Zip code is invalid.")] // US or Canada
    //    [Required(ErrorMessage = "Zip Code is Required.")]
    //    public String ZipCode { set; get; }

    //    public Address(string street, Province province, string zip)
    //    {

    //        this.Street = street;
    //        this.Province = province;
    //        this.ZipCode = zip;
    //    }

    //}

    public class Client
    {
        public Client()
        {
            Projects = new HashSet<Project>();
        }

        [Key]
        public int cliID { get; set; }

        [Display(Name = "Client Name")]
        [Required(ErrorMessage = "Client name is required")]
        [StringLength(50, ErrorMessage = "Client name cannot be more than 50 characters long.")]
        public string cliName { get; set; }

        //[Display(Name = "Address")]
        //[Required(ErrorMessage = "Client address is required")]
        //[StringLength(200, ErrorMessage = "Client address cannot be more than 200 characters long.")]
        //public string cliAddress { get; set; }

        [Display(Name = "Contact Name")]
        [Required(ErrorMessage = "Please provide a contact")]
        [StringLength(50, ErrorMessage = "Contact Name cannot be more than 50 characters long.")]
        public string cliContactName { get; set; }

        [Display(Name = "Contact Type")]
        [StringLength(50, ErrorMessage = "Contact Type cannot be more than 50 characters long.")]
        public string clicontactType { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 cliPhonenumber { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Client street address is required")]
        [RegularExpression(@"^[a-zA-Z 0-9]+$", ErrorMessage = "Street Address not valid.")]
        [StringLength(100, ErrorMessage = "Client address cannot be more than 100 characters long.")]
        public string Street { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City address is required")]
        [RegularExpression(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$", ErrorMessage = "City not valid.")]
        [StringLength(50, ErrorMessage = "Client city cannot be more than 50 characters long.")]
        public string cliCity { get; set; }

        public Province Province { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression("(^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$)", ErrorMessage = "Zip code is invalid.")] // US or Canada
        [Required(ErrorMessage = "Zip Code is Required.")]
        public String ZipCode { set; get; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }//Added for concurrency

        public ICollection<Project> Projects { get; set; }

        public string Address
        {
            get
            {
                return $"{Street}, {cliCity}, {Province}, {ZipCode}";
            }
        }
    }
}
