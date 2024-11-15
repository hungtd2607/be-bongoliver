using BongOliver.Models;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.CategoryRepository;
using BongOliver.Repositories.RoleRepository;
using BongOliver.Repositories.ServiceRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.AccountService;
using BongOliver.Services.AuthService;
using BongOliver.Services.BookingService;
using BongOliver.Services.CategoryService;
using BongOliver.Services.MailService;
using BongOliver.Services.RoleService;
using BongOliver.Services.ServiceService;
using BongOliver.Services.TokenService;
using BongOliver.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<IServiceRepository, ServiceRepository>();
services.AddScoped<IBookingRepository, BookingRepository>();

services.AddScoped<IBookingService, BookingService>();
services.AddScoped<IServiceService, ServiceService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<IMailService, MailService>();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IAccountService, AccountService>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IUserService, UserService>();


//services.AddAutoMapper(typeof(MapperProfile).Assembly);
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });

services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:3000")
            .WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

// Add seed data
using var scope = app.Services.CreateScope();
var servicesProvider = scope.ServiceProvider;
//try
//{
//    var context = servicesProvider.GetRequiredService<DataContext>();
//    context.Database.Migrate();
//    Seed.SeedUsers(context);
//}
//catch (Exception e)
//{
//    var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
//    logger.LogError(e, "Migration failed");
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
