using JwtAspNet;
using JwtAspNet.Extensions;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", p => p.RequireRole("admin"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", handler: (TokenService service) => 
{
    return service.Create(
        new User(1, "jubileu@gmail.com", "Jubileu", "AquiMinhaFoto", "14541", new[] { "student", "premium" })); 
});

app.MapGet("/restrito",(ClaimsPrincipal user) => new
{
    id = ClaimTypesExtension.Id(user),
    name = ClaimTypesExtension.Name(user),
    email = ClaimTypesExtension.Email(user),
    givenname = ClaimTypesExtension.GivenName(user),
    image = ClaimTypesExtension.Image(user),
}).RequireAuthorization();

app.MapGet("/admin", () =>
{
    return "FUNCIONOU COMO ADM";
}).RequireAuthorization("Admin");

app.Run();
