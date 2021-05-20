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
using WMS_API._Repositories.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API._Repositories.Repositories.WMSF.FG_REPORT_COMPARE;
using WMS_API._Services.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API._Services.Services.WMSF.FG_REPORT_COMPARE;
using WMS_API.Data.WMSF.FG_REPORT_COMPARE;
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

            services.AddCors();

            //AddDbContext
            services.AddDbContext<DBContext_WMS>(q => q.UseSqlServer(Configuration.GetConnectionString("TSH_WMSConnection")));
            services.AddDbContext<DBContext_EEP>(q => q.UseSqlServer(Configuration.GetConnectionString("TSH_EEPConnection")));

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp => new Mapper(AutoMapperConfig.RegisterMappings()));
            services.AddSingleton(AutoMapperConfig.RegisterMappings());

            //AddAutoMapper
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp => new Mapper(AutoMapperConfig.RegisterMappings()));
            services.AddSingleton(AutoMapperConfig.RegisterMappings());

            // services.AddScoped<IRepository, Repository>();
            services.AddScoped<IWMSF_FG_CompareReportRepository, WMSF_FG_CompareReportRepository>();
            services.AddScoped<IFRI_PORepository, FRI_PORepository>();

            // services.AddScoped<IService, Service>();
            services.AddScoped<IWMSF_FG_CompareReportService, WMSF_FG_CompareReportService>();
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
