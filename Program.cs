using AutoMapper;
using Microsoft.EntityFrameworkCore;
using IdentityUserRegistration;
using IdentityUserRegistration.Entities;
using Microsoft.AspNetCore.Identity;

// dotnet ef migrations add initial
// dotnet ef database update --connection "User ID=ioncazacu;Password=password;Host=localhost;Port=5432;Database=IdentityUserRegistration;Pooling=true"
// dotnet ef database update --connection "Host=localhost;Port=5432;Database=IdentityUserRegistration;Username=ioncazacu;Pooling=true"

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

builder.Services.AddDbContext<DatabaseContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(o =>
    {
        o.Password.RequiredLength = 7;
        o.Password.RequireDigit = false;
        o.Password.RequireUppercase = false;
        o.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();