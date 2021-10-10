using Hangfir_Scheduler.DBContext;
using Hangfir_Scheduler.ServiceImlementation;
using Hangfir_Scheduler.ServiceInterface;
using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using HangfireBasicAuthenticationFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfir_Scheduler.Models;

namespace Hangfir_Scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hangfire", Version = "v1" });
            });

         
            services.AddDbContext<HanfireDbContext>(item =>
            item.UseSqlServer(Configuration.GetConnectionString("myconn")));
       

           
            services.AddHangfire(c => c.UseSqlServerStorage(Configuration.GetConnectionString("myconn")));
            GlobalConfiguration.Configuration.UseSqlServerStorage(Configuration.GetConnectionString("myconn")).WithJobExpirationTimeout(TimeSpan.FromDays(7));


            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IHangfireTestingService, HangfireTestingService>();
            services.AddTransient<IMailService, MailService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hangfire v1"));
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = null,
                DashboardTitle = "Hangfire Dashboard",
                Authorization = new[]{
                new HangfireCustomBasicAuthenticationFilter{
                    User = Configuration.GetSection("HangfireCredentials:UserName").Value,
                    Pass = Configuration.GetSection("HangfireCredentials:Password").Value
                }
            },
            }); 

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
