using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMovies
{
    public class Dummy
    {
    }

    public static class DummyExtensions
    {
        public static void RegisterDummy(this IServiceCollection services)
        {
            services.AddScoped<Dummy>();
        }
    }
}
