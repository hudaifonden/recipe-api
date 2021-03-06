﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace recipe_api.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BodyRewindMiddleware
    {
        private readonly RequestDelegate _next;

        public BodyRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try { httpContext.Request.EnableBuffering(); } catch { }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BodyRewindMiddlewareExtensions
    {
        public static IApplicationBuilder UseBodyRewindMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BodyRewindMiddleware>();
        }
    }
}
