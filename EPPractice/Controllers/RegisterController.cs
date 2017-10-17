using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPCommon.Contracts;
using EPCommon.Helpers;
using EPCommon.ViewModels;

namespace EPPractice.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegister _register;

        private readonly IEmailSender _emailSender;
        public RegisterController(IRegister register, IEmailSender emailSender)
        {
            _register = register;
            _emailSender = emailSender;
        }
        /// <summary>
        /// Register Index Page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.BusinessSegments = GetBusinessSegmentTypes();
            var areaSelectList = GetBusinessAreaTypes().Select(x=> new SelectListItem()
            {
                Text = x.BusinessAreaType,
                Value = x.Id.ToString()
            });
            ViewBag.BusinessAreas = areaSelectList.ToList();
            return View("Index");
        }

        /// <summary>
        /// Register Company
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Register(RegisterModel model)
        {
            string result;
            if (ModelState.IsValid)
            {
               result = _register.RegisterCompany(model);
                if (string.IsNullOrWhiteSpace(result))  // if no error message send email if it is enabled in config
                {
                    _emailSender.SendMail(model.ContactInfo.Contact.EmailId, "New Client Registered!", "You have been added successfully in travel.com.");
                }
            }
            else
            {
                result = Constants.InvalidDataError;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add new client contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult AddArea(int id)
        {
           var data = GetBusinessAreaTypes().FirstOrDefault(x => x.Id == id);
           var clientContactData = new ClientContactModel()
           {
               BusinessAreaTypeId = id,
               BusinessAreaType = data?.BusinessAreaType
           };
            return PartialView("_ClientContactView", clientContactData);
        }

        /// <summary>
        /// Get constant business segment and restore or casche result for long time 
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 86400, VaryByParam = "None")]
        public List<BusinessSegmentTypesModel> GetBusinessSegmentTypes()
        {
            return _register.GetBusinessSegmentTypes();
        }

        /// <summary>
        /// Get constant business area and restore or casche result for long time 
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 86400, VaryByParam = "None")]
        public List<BusinessAreaTypesModel> GetBusinessAreaTypes()
        {
            return _register.GetBusinessAreaTypes();
        }
    }
}