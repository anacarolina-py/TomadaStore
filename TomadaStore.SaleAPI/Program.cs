using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repository;
using TomadaStore.SaleAPI.Repository.Interfaces;
using TomadaStore.SaleAPI.Services.v1;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<ConnectionDB>();

builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddHttpClient("ProductAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/v1/customer/get/{id}");
});
builder.Services.AddHttpClient("CustomerAPI", client =>
{
    client.BaseAddress = new Uri("https://locahost:6001/api/v1/product/get/{id}");
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
