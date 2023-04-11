using Hangfire;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Configuration;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Services;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Services.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<ChatJob>();

builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.ConfigureHangfire(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseHangfireDashboard();

app.Run();