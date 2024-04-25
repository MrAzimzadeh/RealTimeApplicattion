using RealTimeDB.Hubs;
using RealTimeDB.Models;
using RealTimeDB.Subscription;
using RealTimeDB.Subscription.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<DatabaseSubscription<Satislar>>();
builder.Services.AddSingleton<DatabaseSubscription<Personeller>>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseDatabaseSubscription<DatabaseSubscription<Satislar>>("Satislar");
app.UseDatabaseSubscription<DatabaseSubscription<Personeller>>("Personeller");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<SatisHub>("/satishub");

app.MapControllers();

app.Run();