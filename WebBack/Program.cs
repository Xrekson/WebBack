using WebBack.Entity;
using WebBack.Utility;
using WebBack.Database;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//This section below is for connection string 
builder.Services.AddDbContext<Dbconnect>();
builder.Services.AddScoped<Service>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EncService>(serviceProvider => {
    var envVars = DotEnv.Read();
    var key = envVars["Encryption_Key"];
    var iv = envVars["Encryption_IV"];
    return new EncService(key, iv);
});
builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        var envVars = DotEnv.Read();
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(envVars["JWT_TOKEN"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}else{
app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();