using Microsoft.OpenApi.Models;
using SociedadePrimitivaIO.Chatting.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chatting API", Description = "Chatting API", Version = "v1" });
});

builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.ConfigureMongo(builder.Configuration);
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chatting API");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
