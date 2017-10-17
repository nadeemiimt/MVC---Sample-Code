using System.ComponentModel.DataAnnotations;

namespace EPCommon.ViewModels
{
    public class ContactInfoModel
    {
        public ContactInfoModel()
        {
            ModulesInterestedIn = 1; // hardcoded as functionality not defined
            Contact = new ContactModel();
        }
        public ContactModel Contact { get; set; }

        [Display(Name = "Business Segments: ")]
        public int BusinessSegmentId { get; set; }

        [Display(Name = "Modules Interested In")]
        public int ModulesInterestedIn { get; set; }

        [Display( Name = "Website Url")]
        [StringLength(100)]
        public string WebSiteUrl { get; set; }
    }
}
