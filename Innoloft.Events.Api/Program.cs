using Innoloft.Events.Api.Data.Context;
using Innoloft.Events.Api.Extensions;
using Innoloft.Events.Api.Models;
using Innoloft.Events.Api.Repository;
using Innoloft.Events.Api.Services.Interfaces;
using Innoloft.Events.Api.Services.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var config = builder.Configuration;
var connectionString = config.GetConnectionString("DefaultConnection");
// Add services to the container.
services.AddDbContext<EventsDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
services.AddRedisStorage(new RedisConfiguration
{
    Database = config.GetValue<int>("Redis:Database"),
    Server = config.GetValue<string>("Redis:Server"),
    Port = config.GetValue<string>("Redis:Port")
});
services.AddScoped<IInvitationService ,InvitationService>();

services.AddScoped<IEventRepository ,EventRepository>();
services.AddScoped<IUserRepository ,UserRepository>();
services.AddScoped<IEventParticipantRepository ,EventParticipantRepository>();
services.AddScoped<IEventService ,EventService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
await app.RunMigrationsAsync();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();