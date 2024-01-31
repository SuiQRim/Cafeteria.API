using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfitTest_Cafeteria.API;
using ProfitTest_Cafeteria.API.DataBase;
using ProfitTest_Cafeteria.API.Middlewares;
using ProfitTest_Cafeteria.API.Services;
using ProfitTest_Cafeteria.API.Services.Repositories;
using ProfitTest_Cafeteria.API.Services.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policy =>
		{
			policy.WithOrigins(builder.Configuration.GetSection("WebsiteOrigin").Value!)
				.AllowCredentials()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

builder.Services.AddLogging();

builder.Services.AddDbContext<FoodstuffsApiContext>(
	o => o.UseInMemoryDatabase("FoodstuffsDB"));

builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodCatalogRepository, FoodCatalogRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
	options.RequireHttpsMetadata = true;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = AuthOptions.ISSUER,
		ValidateAudience = true,
		ValidAudience = AuthOptions.AUDIENCE,
		ValidateLifetime = true,
		IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
		ValidateIssuerSigningKey = true,
	};
});

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

app.UseCors();

app.UseHttpsRedirection();


app.UseCookiePolicy(new CookiePolicyOptions
{
	MinimumSameSitePolicy = SameSiteMode.Strict,
	HttpOnly = HttpOnlyPolicy.Always,
	Secure = CookieSecurePolicy.Always
});
app.UseMiddleware<CookieJWTAuthentication>();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
