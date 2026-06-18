using Serilog;
using System.Diagnostics;

namespace DemoVS
{ 
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }

    public class LogTempoChamada
    {
        private readonly RequestDelegate _next;


        public LogTempoChamada(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var sw = Stopwatch.StartNew();

            await _next(httpContext);

            sw.Stop();

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            Log.Information($"A execução demorou {sw.Elapsed.TotalMilliseconds}ms ({sw.Elapsed.TotalSeconds}s)");
        }
    }

    public static class EncapsulaSerilog
    {
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();
        }
    }

}