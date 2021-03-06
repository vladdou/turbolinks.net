using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Turbolinks.Net
{
    public class TurbolinksMiddleware : IMiddleware
    {
        public const string TurbolinksLocationHeader 
            = "Turbolinks-Location";

        public async Task InvokeAsync(
            HttpContext httpContext, 
            RequestDelegate next
        )
        {
            httpContext.Response.OnStarting((state) => {
                if (state is HttpContext ctx) 
                {
                    if (ctx.IsTurbolinksRequest())
                    {
                        ctx.Response.Headers.Add(TurbolinksLocationHeader, ctx.Request.GetEncodedUrl());
                    }
                }

                return Task.CompletedTask;
            }, httpContext);
            
            await next(httpContext);
        }
    }
}

