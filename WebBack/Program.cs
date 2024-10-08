using WebBack.Entity;
using WebBack.Encryption;
using Microsoft.EntityFrameworkCore;
using WebBack.Database;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//This section below is for connection string 
builder.Services.AddDbContext<Dbconnect>();
builder.Services.AddScoped<Service>();
builder.Services.AddScoped<EncService>(serviceProvider => {
    var envVars = DotEnv.Read();
    var key = envVars["Encryption_Key"];
    var iv = envVars["Encryption_IV"];
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