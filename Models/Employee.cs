using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Models
{
    public class Employee
    {
        public Employee()
        {
            SalesBids = new HashSet<Bid>();
            DesignerBids = new HashSet<Bid>();
        }

        [Key]
        public int empID { get; set; }

        [Display(Name = "Employee First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string empFirst { get; set; }

        [Display(Name = "Employee Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string empLast { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 empPhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string eMail { get; set; }

        [Display(Name = "Employee Type")]
        [Required(ErrorMessage = "You must select an employee type.")]
        public string empType { get; set; }

        [Display(Name = "Employee")]
        public string empFullName
        {
            get
            {
                return empFirst + " " + empLast ;
            }
        }

        [InverseProperty("Designer")]
        public ICollection<Bid> DesignerBids { get; set; }

        [InverseProperty("SalesAssociate")]
        public ICollection<Bid> SalesBids { get; set; }
    }
}
