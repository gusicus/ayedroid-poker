using Ayedroid.Poker.Classes;
using Ayedroid.Poker.Exceptions;
using Ayedroid.Poker.Interfaces;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISessionContainer>(serviceProvider => new SessionContainer(serviceProvider.GetRequiredService<ILogger<SessionContainer>>()));

var app = builder.Build();

app.UseExceptionHandler(a => a.Run(async context =>
{
    IExceptionHandlerPathFeature? exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    Exception? exception = exceptionHandlerPathFeature?.Error;

    int statusCode = 500;
    string error = "Unknown error";
    // use default if null
    if (exception is null) { }
    else if (exception is SessionNotFoundException) { statusCode = StatusCodes.Status404NotFound; error = "Session does not exist"; }
    else if (exception is ArgumentNullException) { statusCode = StatusCodes.Status400BadRequest; error = "All fields must have a value"; }

    context.Response.StatusCode = statusCode;
    await context.Response.WriteAsJsonAsync(new { error });
}));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
