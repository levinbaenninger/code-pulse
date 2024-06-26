using CodePulse.API.Data;
using CodePulse.API.Repository.Implementation;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("CodePulseConnectionString"));
});
builder.Services.AddDbContext<AuthDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("CodePulseConnectionString"));
});
builder.Services.AddScoped<ICategoryRepository, CategoryRespository>();
builder.Services.AddScoped<IBlogPostRespository, BlogPostRepository>();
builder.Services.AddIdentityCore<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CodePulse")
	.AddEntityFrameworkStores<AuthDbContext>()
	.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequiredLength = 8;
	options.Password.RequiredUniqueChars = 1;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			AuthenticationType = "Jwt",
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
	options.AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
