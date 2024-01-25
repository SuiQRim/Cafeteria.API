using Microsoft.EntityFrameworkCore;
using Profit_Food.API.DataBase;
using Profit_Food.API.DataBase.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FoodstuffsApiContext>(
	o => o.UseInMemoryDatabase("FoodstuffsDB"));

builder.Services.AddScoped<IFoodRepository, FoodRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
