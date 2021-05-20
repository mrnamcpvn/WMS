using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
using WMS_API._Repositories.Interfaces;
using WMS_API._Repositories.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API._Repositories.Repositories.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API._Services.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API._Services.Services.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Data.WMSF.FG_TrackingKanban_SortingKanban;
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

            services.AddDbContext<CB_WMSContext>(o => o.UseSqlServer(Configuration.GetConnectionString("CB_WMSConnection")));
            services.AddDbContext<CB_EEPContext>(o => o.UseSqlServer(Configuration.GetConnectionString("CB_EEPConnection")));

            services.AddCors();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp => new Mapper(AutoMapperConfig.RegisterMappings()));
            services.AddSingleton(AutoMapperConfig.RegisterMappings());

            // services.AddScoped<IRepository, Repository>();
            services.AddScoped<IVW_FGIN_LOCAT_LISTRepository, VW_FGIN_LOCAT_LISTRepository>();
            services.AddScoped<IVW_WMS_DepartmentRepository, VW_WMS_DepartmentRepository>();

            // services.AddScoped<IService, Service>();
            services.AddScoped<IVW_FGIN_LOCAT_LISTService, VW_FGIN_LOCAT_LISTService>();
            services.AddScoped<IVW_WMS_DepartmentService, VW_WMS_DepartmentService>();
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
