using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDbContext<ContextBase>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConexaoPadrao") ?? throw new InvalidOperationException("Connection string 'ConexaoPadrao' not found.");
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure"));
});

services.AddScoped<PasswordHasherService>();
services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserRepository, UserRepository>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
