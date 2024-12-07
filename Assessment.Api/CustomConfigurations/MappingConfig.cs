using AutoMapper;

namespace Assessment.Api.CustomConfigurations
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration? mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<ProjectResponseDto, Project>();
              

            });

            return mappingConfig;
        }
    }
}
