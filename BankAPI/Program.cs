using BankAPI.DAL;
using BankAPI.Service;
using BankAPI.Services.Implementations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BankAPI.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext (exempla used SQL Server)
builder.Services.AddDbContext<YouBakingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankDB"))
);

// Register AccountService
builder.Services.AddScoped<IAccountService, AccountService>();
//builder.Services.AddAutoMapper(typeof(BankAPI.Profiles.AutomapperProfiles).Assembly);

builder.Services.AddAutoMapper(typeof(AutomapperProfiles));


//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register controllers
builder.Services.AddControllers();

// Register Swagger (optional)
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
