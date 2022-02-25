using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExamples.Middlewares
{
    public class HelloMiddleware
    {
        private readonly RequestDelegate _next;
        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext context) //Method name must be Invoke or InvokeAsync.
        {
            Console.WriteLine("HelloMiddleware triggered.");
            await _next.Invoke(context);
            Console.WriteLine("Job is done.");
        }
    }
    public static class HelloMiddlewareExtension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}
