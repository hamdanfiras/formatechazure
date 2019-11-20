using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SuperMovies.Models;

namespace SuperMovies
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DB>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<Dummy>();

            services.AddScoped<ClientInfo>();
            services.AddSingleton<JWT>();

            services.AddControllers().AddNewtonsoftJson();

            ConfigureJWT(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("POWEREDBY", "ABOU ELJAMAJEM");
                await next.Invoke();
            });

            app.UseMiddleware<ClientInfoMiddleware>();

            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("POWEREDBY", "ABOU ELJAMAJEM");
            //    await next.Invoke();
            //});

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void ConfigureJWT(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes("BOERlzxLd34kpqtUvu9jr6P5Z4gd0s2QxuHXoWbp");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires == null ? true : DateTime.UtcNow < expires
                };
            });
        }

        //public Task X(HttpContext context, Func<Task> next)
        //{

        //}
    }

    public class ClientInfoMiddleware
    {
        private readonly RequestDelegate next;
        public ClientInfoMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context, ClientInfo clientInfo)
        {
            clientInfo.Culture = context.Request.Headers["Accept-Language"].FirstOrDefault();
            // before next middleware
            await next(context);
            // after next middleware
        }
    }

    public class ClientInfo
    {
        public string Culture { get; set; }
    }
}
