using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRLiveDataProject.Entities;
using SignalRLiveDataProject.Hubs;
using SignalRLiveDataProject.Subscriptions;
using SignalRLiveDataProject.Subscriptions.Middlewares;

namespace SignalRLiveDataProject
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
            services.AddCors(options => options.AddDefaultPolicy(policy =>
            {
                policy.AllowCredentials()
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .SetIsOriginAllowed(x => true);
            }));
            services.AddSignalR();
            services.AddSingleton<DatabaseSubscription<Sale>>();
            services.AddSingleton<DatabaseSubscription<Employee>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseDatabaseSubscription<DatabaseSubscription<Sale>>("Sales");
            app.UseDatabaseSubscription<DatabaseSubscription<Employee>>("Employees");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World");
                // });
                endpoints.MapHub<SalesHub>("/saleshub");
            });
        }
    }
}

