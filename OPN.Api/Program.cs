using Microsoft.EntityFrameworkCore;
using OPN.Data;
using OPN.Data.Repositories;
using OPN.Domain;
using OPN.Domain.Repositories;
using OPN.ExternalConnections.GoogleSheets;
using OPN.Services;
using OPN.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetService<IConfiguration>();
    return new ApplicationContext(configuration!.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITaskService, ProductHandlingTaskService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IProductHandlingTasksRepository, ProductHandlingTasksRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IInstitutionRepository, InstitutionRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProportionsRepository, ProportionsRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => 

    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()

));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseDeveloperExceptionPage();

app.UseAuthorization();

app.UseCors("corspolicy");

app.MapControllers();

app.Run();
