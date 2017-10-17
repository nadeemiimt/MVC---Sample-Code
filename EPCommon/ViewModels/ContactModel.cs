using System.ComponentModel.DataAnnotations;

namespace EPCommon.ViewModels
{
    public class ContactModel
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Your {0} is required")]
        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Your {0} is required")]
        [StringLength(20)]
        public string PhoneNo { get; set; }

        [Display(Name = "Fax Number")]
        [StringLength(20)]
        public string FaxNo { get; set; }

        [Display(Name = "Email Id")]
        [Required(ErrorMessage = "Your {0} is required")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
    }
}
