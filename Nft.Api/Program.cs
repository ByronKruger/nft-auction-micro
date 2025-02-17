using NftApi;
using NftApi.Extensions;
using NftApi.Middleware;
using NftApi.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseFactoryActivatedMiddleware();
// app.UseMiddleware<ErrorHandleMiddleware>();

app.MapHub<BiddingHub>("hubs/bidding");
// app.MapHub<PresenceHub>("hubs/presence");

// CORS
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .AllowCredentials().WithOrigins("http://localhost:4200", "http://localhost:8080"));

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
