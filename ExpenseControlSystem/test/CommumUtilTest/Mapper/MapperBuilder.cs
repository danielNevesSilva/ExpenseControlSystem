using ExpenseControlSystem.Application.AutoMapper;

namespace CommumUtilTest.Mapper
{
    using AutoMapper;

    public static class MapperBuilder
    {
        public static IMapper Build()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });

            mapperConfig.AssertConfigurationIsValid();

            return mapperConfig.CreateMapper();
        }
    }

}
