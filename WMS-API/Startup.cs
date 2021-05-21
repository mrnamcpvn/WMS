using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Machine_API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WMS_API._Repositories;
using WMS_API._Repositories.Interfaces;
using WMS_API._Repositories.Repositories;
using WMS_API._Services.Interface;
using WMS_API._Services.Services;
using WMS_API.Data;
using WMS_API.Helpers.AutoMapper;

namespace WMS_API
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

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WMS_API", Version = "v1" });
            });
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<SHCQDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CB_EEPConnection")));
            services.AddCors();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp => new Mapper(AutoMapperConfig.RegisterMappings()));
            services.AddSingleton(AutoMapperConfig.RegisterMappings());

            //repo
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWMS_LocationRepository, WMS_LocationRepository>();
            services.AddTransient<IWMSF_Carton_LocatRepository, WMSF_Carton_LocatRepository>();
            services.AddTransient<IWMSF_Rack_AreaRepository, WMSF_Rack_AreaRepository>();

            //Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IWMS_LocationService, WMS_LocationService>();
            services.AddTransient<IWMSF_Carton_LocatService, WMSF_Carton_LocatService>();
            services.AddTransient<IWMSF_Rack_AreaService, WMSF_Rack_AreaService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WMS_API v1"));
            }

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
