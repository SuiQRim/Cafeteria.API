using Microsoft.EntityFrameworkCore;
using ProfitTest_Cafeteria.API.DataBase;
using ProfitTest_Cafeteria.API.Middlewares;
using ProfitTest_Cafeteria.API.Services;
using ProfitTest_Cafeteria.API.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddDbContext<FoodstuffsApiContext>(
	o => o.UseInMemoryDatabase("FoodstuffsDB"));

builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodCatalogRepository, FoodCatalogRepository>();

builder.Services.AddTransient<IFoodCatalogExcel, FoodCatalogExcel>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
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

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
