using DemoVS;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.AddSerilog();

app.UseMiddleware<LogTempoChamada>();

app.MapGet("/", () => "Hello World!");

app.MapGet("/teste", () =>
{
    Thread.Sleep(1200);
    return "123 testando";
});

app.Run();