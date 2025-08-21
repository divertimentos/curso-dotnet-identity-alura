using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var connString = builder.Configuration["ConnectionStrings:UsuarioConnection"];

builder.Services.AddDbContext<UsuarioDbContext>(opts =>
{
    opts.UseMySql(connString, ServerVersion.AutoDetect(connString));
});

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

// Configures Automapper (the newest version, with the latest syntax)
builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

// Configs de injeção de dependência / instanciação para cada service usada nas controllers.
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();


// Access Policy
builder.Services
    .AddAuthentication(options => { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"]!)),
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy => { policy.AddRequirements(new IdadeMinima(18)); });
});


builder.Services.AddControllers();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();