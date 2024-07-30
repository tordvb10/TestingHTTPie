using TestingHTTPie.Data;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using TestingHTTPie.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using TestingHTTPie.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }).AddNewtonsoftJson();

// Add the custom JSON Patch input formatter

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

//builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IHobbyRepository, HobbyRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Register the Swagger generator, defining one or more Swagger documents
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextTestingHTTPie>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();