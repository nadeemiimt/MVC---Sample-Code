using System.ComponentModel.DataAnnotations;

namespace EPCommon.ViewModels
{
    public class CompanyInfoModel : BaseModel
    {
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Your {0} is required")]
        [StringLength(100)]
        public string ClientName { get; set; }

        [Display(Name = "Company Type")]
        [StringLength(100)]
        public string CompanyType { get; set; }

        [Display(Name = "Year of Foundation")]
        [StringLength(4)]
        public string FoundationYear { get; set; }

        [Display(Name = "No. of Employees")]
        [StringLength(12)]
        public string TotalEmployees { get; set; }

        [Required(ErrorMessage = "Your {0} is required")]
        [StringLength(250)]
        public string City { get; set; }

        [Required(ErrorMessage = "Your {0} is required")]
        [StringLength(250)]
        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Your {0} is required")]
        [StringLength(10)]
        public string PostCode { get; set; }

    }
}
