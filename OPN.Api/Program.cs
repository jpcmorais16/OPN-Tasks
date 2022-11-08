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


//private static readonly ISpreadsheetConnection _connection = new GoogleSheetsConnection(@"C:\Users\Trilogo\Desktop\credentials\credentials.json");
//private static readonly SpreadsheetDataFetcher _dataFetcher = new SpreadsheetDataFetcher(_connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw");
builder.Services.AddSingleton<ISpreadsheetConnection, GoogleSheetsConnection>();
builder.Services.AddSingleton<IProductHandlingTaskDataFetcher, SpreadsheetDataFetcher>(provider => {
    return new SpreadsheetDataFetcher(provider.GetRequiredService<ISpreadsheetConnection>(), "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw");
});
builder.Services.AddSingleton<IProductHandlingTaskDataCommiter, SpreadsheetDataCommiter>(provider => new SpreadsheetDataCommiter(provider.GetRequiredService<ISpreadsheetConnection>(), "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"));
builder.Services.AddSingleton<IUserDataFetcher, SpreadsheetDataFetcher>(provider =>  new SpreadsheetDataFetcher(provider.GetRequiredService<ISpreadsheetConnection>(), "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"));
builder.Services.AddSingleton<IUserDataCommiter, SpreadsheetDataCommiter>(provider => new SpreadsheetDataCommiter(provider.GetRequiredService<ISpreadsheetConnection>(), "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"));
builder.Services.AddSingleton<ITaskService, TaskService>();
builder.Services.AddSingleton<ILoginService, LoginService>();



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
