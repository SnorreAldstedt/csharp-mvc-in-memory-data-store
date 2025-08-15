using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Reflection.Metadata.Ecma335;

using exercise.wwwapi.Data;
using exercise.wwwapi.Repository;
using exercise.wwwapi;
using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddOpenApi();

builder.Services.AddOpenApi();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("productsdb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "Product API"); });

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoints();

app.Run();
