using ExpenseControlSystem.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseControlSystem.Infrastructure.UoW
{
    public class NativeInjectorBootStrapper
    {
        public static void RegiiterServices(IServiceCollection service)
        {
            service.AddDbContext<Context>();
        }
    }
}
