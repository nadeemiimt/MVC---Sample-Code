using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPCommon.Contracts;
using EPCommon.Helpers;
using EPCommon.ViewModels;
using EPPractice.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EPPractice.Tests.Controllers
{
    [TestClass]
    public class RegisterControllerTest
    {
        private IRegister _register;
        private IEmailSender _emailSender;
        [TestInitialize]
        public void Initialize()
        {
            _register = Substitute.For<IRegister>();
            var registerBusinessSegmentTypes = Substitute.For<List<BusinessSegmentTypesModel>>();
            _register.GetBusinessSegmentTypes().Returns(registerBusinessSegmentTypes);
            var registerBusinessAreaTypes = Substitute.For<List<BusinessAreaTypesModel>>();
            _register.GetBusinessAreaTypes().Returns(registerBusinessAreaTypes);
            _emailSender = Substitute.For<IEmailSender>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _register = null;
            _emailSender = null;
        }
        [TestMethod]
        public void RegisterController_IndexActionCalled__ReturnsIndexView()
        {
            RegisterController controllerUnderTest = new RegisterController(_register, _emailSender);
            var result = controllerUnderTest.Index() as ViewResult;
            Assert.AreEqual("Index", result?.ViewName);
        }

        [TestMethod]
        public void RegisterController_IndexActionCalled_ViewBagsAreSet()
        {
            var segments = SetRegisterBusinessSegmentTypes();
            var areasSelectListItems = SetRegisterBusinessAreaTypes().Select(x => new SelectListItem()
            {
                Text = x.BusinessAreaType,
                Value = x.Id.ToString()
            });

            RegisterController controllerUnderTest = new RegisterController(_register, _emailSender);
            var result = controllerUnderTest.Index() as ViewResult;
            Assert.AreEqual(segments, result.ViewData["BusinessSegments"]);

            Assert.IsTrue(AreEqual(areasSelectListItems.ToList(), result.ViewData["BusinessAreas"] as List<SelectListItem>));

        }

        [TestMethod]
        public void RegisterController_RegisterActionCalledAndModelStateIsValid_RegisterWillBeCalled()
        {
            _register.RegisterCompany(Arg.Any<RegisterModel>()).Returns("");
            var registerCallCount = 0;
            _emailSender.When(x => x.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).DoNotCallBase();
            _register.When(x=>x.RegisterCompany(Arg.Any<RegisterModel>())).Do(x =>
            {
                registerCallCount++;
            });
            RegisterController controllerUnderTest = new RegisterController(_register, _emailSender);
            controllerUnderTest.ModelState.Clear();
            var registerModel = GetRegisterModel();

            controllerUnderTest.Register(registerModel);

            Assert.AreEqual(registerCallCount, 1);
        }

        [TestMethod]
        public void RegisterController_RegisterActionCalledAndModelStateIsValidAndAddIsSuccess_EmailWillBeSend()
        {
            _register.RegisterCompany(Arg.Any<RegisterModel>()).Returns("");
            var emailCallCount = 0;
            _emailSender.When(x => x.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => emailCallCount++);
            RegisterController controllerUnderTest = new RegisterController(_register, _emailSender);
            controllerUnderTest.ModelState.Clear();
            var registerModel = GetRegisterModel();

            controllerUnderTest.Register(registerModel);

            Assert.AreEqual(emailCallCount, 1);

        }

        [TestMethod]
        public void RegisterController_RegisterActionCalledAndModelStateIsInValid_RegisterMethodWillNotBeCalled()
        {
            _register.RegisterCompany(Arg.Any<RegisterModel>()).Returns("");
            var registerCallCount = 0;
            _emailSender.When(x => x.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).DoNotCallBase();
            _register.When(x => x.RegisterCompany(Arg.Any<RegisterModel>())).Do(x =>
            {
                registerCallCount++;
            });
            RegisterController controllerUnderTest = new RegisterController(_register, _emailSender);
            controllerUnderTest.ModelState.AddModelError("test", "test");
            var registerModel = GetRegisterModel();

            controllerUnderTest.Register(registerModel);

            Assert.AreEqual(registerCallCount, 0);

        }

        [TestMethod]
        public void RegisterController_RegisterActionCalledAndModelStateIsInValid_ResultWillBeInValidData()
        {
            _emailSender.When(x => x.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).DoNotCallBase();
            RegisterController controllerUnderTest = new RegisterController(_register, _emailSender);
            controllerUnderTest.ModelState.AddModelError("test", "test");
            var registerModel = GetRegisterModel();

            var result = controllerUnderTest.Register(registerModel) as JsonResult;

            Assert.IsTrue(result.Data.ToString() == Constants.InvalidDataError);

        }

        private RegisterModel GetRegisterModel()
        {
            return new RegisterModel()
            {
                ContactInfo = new ContactInfoModel()
                {
                    Contact = new ContactModel()
                    {
                        ContactName = "Test1",
                        Designation = "Test",
                        EmailId = "test@test.com",
                        PhoneNo = "1999999999",
                        FaxNo = "43445555"
                    },
                    BusinessSegmentId = 1,
                    WebSiteUrl = "www.google.com",
                    ModulesInterestedIn = 1
                },
                CompanyInfo = new CompanyInfoModel()
                {
                    ClientName = "Test",
                    City = "Test",
                    FoundationYear = "2017",
                    CompanyType = "Test",
                    PostCode = "111111",
                    Address = "Test",
                    TotalEmployees = "22",
                    UpdatedAt = DateTime.Now,
                    CreatedBy = 1234,
                    UpdatedBy = 1234,
                    CreatedAt = DateTime.Now
                },
                BusinessAreas = new List<ClientContactModel>()
                {
                    new ClientContactModel()
                    {
                        ContactName = "Test1",
                        BusinessAreaTypeId = 1,
                        Designation = "Test",
                        EmailId = "t@t.com",
                        BusinessAreaType = "333",
                        PhoneNo = "2222233",
                        FaxNo = "444444",
                        UpdatedAt = DateTime.Now,
                        CreatedBy = 1234,
                        UpdatedBy = 1234,
                        CreatedAt = DateTime.Now
                    }
                }
            };
        }
        private List<BusinessSegmentTypesModel> SetRegisterBusinessSegmentTypes()
        {
            var registerBusinessSegmentTypes = new List<BusinessSegmentTypesModel>()
            {
                new BusinessSegmentTypesModel() { Id = 1, SegmentType = "Test1"},
                new BusinessSegmentTypesModel() { Id = 2, SegmentType = "Test2"},
                new BusinessSegmentTypesModel() { Id = 3, SegmentType = "Test3"},
                new BusinessSegmentTypesModel() { Id = 4, SegmentType = "Test4"},
                new BusinessSegmentTypesModel() { Id = 5, SegmentType = "Test5"}
            };
            _register.GetBusinessSegmentTypes().Returns(registerBusinessSegmentTypes);
            return registerBusinessSegmentTypes;
        }

        private List<BusinessAreaTypesModel> SetRegisterBusinessAreaTypes()
        {
            var registerBusinessAreaTypes = new List<BusinessAreaTypesModel>()
            {
                new BusinessAreaTypesModel() { Id = 1, BusinessAreaType = "Test1", OtherDescription = "Test11"},
                new BusinessAreaTypesModel() { Id = 2, BusinessAreaType = "Test2", OtherDescription = "Test22"},
                new BusinessAreaTypesModel() { Id = 3, BusinessAreaType = "Test3", OtherDescription = "Test33"},
                new BusinessAreaTypesModel() { Id = 4, BusinessAreaType = "Test4", OtherDescription = "Test44"},
            };
            _register.GetBusinessAreaTypes().Returns(registerBusinessAreaTypes);
            return registerBusinessAreaTypes;
        }

        private static bool AreEqual<T>(List<T> list1, List<T> list2)
        {
            if (list1.Count != list2.Count) return false;
            for (var i = 0; i < list1.Count; i++)
            {
                if (!CommonHelpers.AreObjectsEqual(list1[i], list2[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
