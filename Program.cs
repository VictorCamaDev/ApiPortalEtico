using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using ApiPortalEtico.API.Middleware;
using ApiPortalEtico.Infrastructure;
using ApiPortalEtico.Application;
using ApiPortalEtico.Application.Emails.Templates;

var builder = WebApplication.CreateBuilder(args);

// Asegurarse de que la carpeta wwwroot existe
string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);

    // Crear la carpeta de imágenes si no existe
    string imagesPath = Path.Combine(wwwrootPath, "images");
    if (!Directory.Exists(imagesPath))
    {
        Directory.CreateDirectory(imagesPath);
    }
}

// Add services to the container
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ApiPortalEtico.Infrastructure")));

// Register application services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean Architecture API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Crea BD y aplica migraciones
        Console.WriteLine("Base de datos actualizada exitosamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al aplicar migraciones:");
        Console.WriteLine(ex.Message);
    }
}

// Initialize EmailTemplateService with WebHostEnvironment
EmailTemplateService.Initialize(app.Environment);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture API v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Importante: Habilita el acceso a archivos estáticos
app.UseRouting();
app.UseCors("AllowReactApp");

// Add custom exception handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

