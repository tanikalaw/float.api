using Float.Api.Extensions;
using Float.Api.Middleware;
using Float.Application;
using Float.Infrastracture.Identity;
using Float.Infrastracture.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Float.Api
{
    public class Startup
    {

        //TODO: Seperate all configs per layer for better organization of classes per layer
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration _config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplicationLayer();
            services.AddIdentityInfrastracture(_config);
            services.AddPersistenceInfrastracture(_config);
            services.AddSwaggerExtension();
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseExceptionMiddleWare();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
