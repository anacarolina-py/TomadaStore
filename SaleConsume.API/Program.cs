using SaleConsume.API.Repository;
using SaleConsume.API.Service;
using TomadaStore.SaleAPI.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<SaleConsumerRepository>();
builder.Services.AddScoped<SaleConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
