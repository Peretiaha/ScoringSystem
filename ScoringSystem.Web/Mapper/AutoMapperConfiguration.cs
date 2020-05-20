using AutoMapper;

namespace ScoringSystem.Web.Mapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Create()
        {
            var config = new MapperConfiguration(
                x =>
                {
                    x.AddProfile<MappingProfile>();
                });
            return config;
        }
    }
}
