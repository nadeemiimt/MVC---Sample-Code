using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPCommon.ViewModels
{
    public class RegisterModel 
    {
        public RegisterModel()
        {
            CompanyInfo = new CompanyInfoModel();
            ContactInfo = new ContactInfoModel();
            BusinessAreas = new List<ClientContactModel>();
        }
        [Required(ErrorMessage = "Your {0} is required")]
        public CompanyInfoModel CompanyInfo { get; set; }

        [Required(ErrorMessage = "Your {0} is required")]
        public ContactInfoModel ContactInfo { get; set; }

        [Display(Name = "Business Areas: ")]
        public List<ClientContactModel> BusinessAreas { get; set; }
    }
}
