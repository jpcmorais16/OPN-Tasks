using OPN.Data.GoogleSheets;
using OPN.Data.SpreadSheets;
using OPN.Data.SpreadSheets.Interfaces;
using OPN.Domain.Interfaces;
using OPN.ExternalConnections.GoogleSheets;
using OPN.Services;
using OPN.Services.Interfaces;
using OPN.Services.Interfaces.DataInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISpreadsheetConnection, GoogleSheetsConnection>();
builder.Services.AddSingleton<IProductHandlingTaskDataFetcher, SpreadsheetDataFetcher>();
builder.Services.AddSingleton<IProductHandlingTaskDataCommiter, SpreadsheetDataCommiter>();
builder.Services.AddSingleton<IUserDataFetcher, SpreadsheetDataFetcher>();
builder.Services.AddSingleton<IUserDataCommiter, SpreadsheetDataCommiter>();
builder.Services.AddSingleton<ITaskService, TaskService>();



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