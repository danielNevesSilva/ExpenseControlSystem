using AutoMapper;
using ExpenseControlSystem.Application.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommumUtilTest.Mapper
{
    using AutoMapper;
    using Microsoft.Extensions.Logging.Abstractions;

    public static class MapperBuilder
    {
        public static IMapper Build()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            }, NullLoggerFactory.Instance);

            mapperConfig.AssertConfigurationIsValid();

            return mapperConfig.CreateMapper();
        }
    }

}
