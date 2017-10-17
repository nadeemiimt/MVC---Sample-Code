using AutoMapper;
using EPBusinessLogic.Helpers.Mappers;

namespace EPPractice.Helpers.Mappers
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MvcMappingProfile());  //mapping between MVC and Business layer objects
                cfg.AddProfile(new BusinessMappingProfile());  // mapping between Business and DB layer objects
            });

            return config;
        }
    }
}