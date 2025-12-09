using SaleConsume.API.Repository;
using SaleConsume.API.Repository.Interfaces;
using SaleConsume.API.Service;
using SaleConsume.API.Service.Interfaces;
using TomadaStore.SaleAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddScoped<ISaleConsumerService, SaleConsumerService>();
builder.Services.AddScoped<ISaleConsumerRepository, SaleConsumerRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
