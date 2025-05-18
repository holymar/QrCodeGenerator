using Microsoft.OpenApi.Models;
using QrCodeGenerator.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QrCodeGenerator API", Version = "v1" });
});

CoreRegistrator.RegisterCoreServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/v1/swagger.json", "QrCodeGenerator API v1");
    c.RoutePrefix = string.Empty;
});
app.MapControllers();

app.Run();