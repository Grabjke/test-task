using OrderService.Framework.Middlewares;
using OrderService.Web;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseExceptionMiddleware();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();