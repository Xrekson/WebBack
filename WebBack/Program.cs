using WebBack.Entity;
using WebBack.Encryption;
using Microsoft.EntityFrameworkCore;
using WebBack.Database;
using MongoDB.Bson;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//This section below is for connection string 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Dbconnect>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<MongoDbConect>(builder.Configuration.GetSection("Mongodatabase"));
builder.Services.AddSingleton<LocationService>();
builder.Services.AddScoped<Service>();
builder.Services.AddScoped<EncService>(serviceProvider => {
    var configuration = serviceProvider.GetService<IConfiguration>();
    var key = configuration["Encryption:Key"];
    var iv = configuration["Encryption:IV"];
    Console.WriteLine(key,iv);
    return new EncService(key, iv);
});
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