using AutoMapper;
using EPCommon.ViewModels;
using EPDataLayer.Entities;

namespace EPBusinessLogic.Helpers.Mappers
{
    public class BusinessMappingProfile : Profile
    {
        public BusinessMappingProfile()
        {
            // Model To Entity Mapping
            CreateMap<RegisterModel, ClientMaster>()
                .ForMember(destination => destination.Address, action => action.MapFrom(source => source.CompanyInfo.Address))
                .ForMember(destination => destination.City, action => action.MapFrom(source => source.CompanyInfo.City))
                .ForMember(destination => destination.ClientName, action => action.MapFrom(source => source.CompanyInfo.ClientName))
                .ForMember(destination => destination.CompanyType, action => action.MapFrom(source => source.CompanyInfo.CompanyType))
                .ForMember(destination => destination.FoundationYear, action => action.MapFrom(source => source.CompanyInfo.FoundationYear))
                .ForMember(destination => destination.PostCode, action => action.MapFrom(source => source.CompanyInfo.PostCode))
                .ForMember(destination => destination.TotalEmployees, action => action.MapFrom(source => source.CompanyInfo.TotalEmployees))
                .ForMember(destination => destination.CreatedBy, action => action.MapFrom(source => source.CompanyInfo.CreatedBy))
                .ForMember(destination => destination.CreatedAt, action => action.MapFrom(source => source.CompanyInfo.CreatedAt))
                .ForMember(destination => destination.UpdatedBy, action => action.MapFrom(source => source.CompanyInfo.UpdatedBy))
                .ForMember(destination => destination.UpdatedAt, action => action.MapFrom(source => source.CompanyInfo.UpdatedAt))
                .ForMember(destination => destination.BusinessSegmentId, action => action.MapFrom(source => source.ContactInfo.BusinessSegmentId))
                .ForMember(destination => destination.ContactName, action => action.MapFrom(source => source.ContactInfo.Contact.ContactName))
                .ForMember(destination => destination.Designation, action => action.MapFrom(source => source.ContactInfo.Contact.Designation))
                .ForMember(destination => destination.EmailId, action => action.MapFrom(source => source.ContactInfo.Contact.EmailId))
                .ForMember(destination => destination.FaxNo, action => action.MapFrom(source => source.ContactInfo.Contact.FaxNo))
                .ForMember(destination => destination.PhoneNo, action => action.MapFrom(source => source.ContactInfo.Contact.PhoneNo))
                .ForMember(destination => destination.Id, action => action.Ignore())
                .ForMember(destination => destination.LogoPath, action => action.Ignore())
                .ForMember(destination => destination.WebSiteUrl, action => action.Ignore());

            CreateMap<ClientContactModel, ClientContact>()
                .ForMember(destination => destination.BusinessAreaTypeId, action => action.MapFrom(source => source.BusinessAreaTypeId))
                .ForMember(destination => destination.ContactName, action => action.MapFrom(source => source.ContactName))
                .ForMember(destination => destination.Designation, action => action.MapFrom(source => source.Designation))
                .ForMember(destination => destination.EmailId, action => action.MapFrom(source => source.EmailId))
                .ForMember(destination => destination.FaxNo, action => action.MapFrom(source => source.FaxNo))
                .ForMember(destination => destination.PhoneNo, action => action.MapFrom(source => source.PhoneNo))
                .ForMember(destination => destination.CreatedAt, action => action.MapFrom(source => source.CreatedAt))
                .ForMember(destination => destination.CreatedBy, action => action.MapFrom(source => source.CreatedBy))
                .ForMember(destination => destination.UpdatedAt, action => action.MapFrom(source => source.UpdatedAt))
                .ForMember(destination => destination.UpdatedBy, action => action.MapFrom(source => source.UpdatedBy))
                .ForMember(destination => destination.Id, action => action.Ignore())
                .ForMember(destination => destination.ClientId, opt => opt.ResolveUsing(
                    (src, dst, arg3, context) => context.Options.Items["ClientId"]
                ));

            // Entity to Model Mapping

            CreateMap<BusinessAreaType, BusinessAreaTypesModel>()
                .ForMember(destination => destination.BusinessAreaType, action => action.MapFrom(source => source.BusinessAreaType1));

            CreateMap<BusinessSegmentType, BusinessSegmentTypesModel>();

        }
    }
}
