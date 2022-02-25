using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MiddlewareExamples.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExamples
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewareExamples", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareExamples v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.Run()

            //app.Run(async context => Console.WriteLine("Middleware 1."));
            //app.Run(async context => Console.WriteLine("Middleware 2."));

            //app.Use()

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware 1 start.");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware 1 finish.");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware 2 start.");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware 2 finish.");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware 3 start.");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware 3 finish.");
            //});

            app.UseHello();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Use Middleware triggered.");
                await next.Invoke();
            });

            app.Map("/example", internalApp => internalApp.Run(async context =>
            {
                Console.WriteLine("/example middleware triggered.");
                await context.Response.WriteAsync("/example middleware triggered.");
            }));

            //app.MapWhen

            app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            {
                internalApp.Run(async context =>
                {
                    Console.WriteLine("MapWhen Middleware triggered.");
                    await context.Response.WriteAsync("MapWhen Middleware triggered.");
                });
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
