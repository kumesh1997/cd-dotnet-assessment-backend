using AutoMapper;
using DataAccess.Entities;
using Model.Dtos;

namespace Assessment.Api.CustomConfigurations
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration? mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateClassDto, Class>();
              

            });

            return mappingConfig;
        }
    }
}
