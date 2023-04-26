using Microsoft.AspNetCore.Builder;
using ZonalTechTest.Application;
using ZonalTechTest.Repository;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(LaunchCommandRepository).Assembly);

builder.Services.AddSingleton<DapperConnectionProvider>();

builder.Services.AddScoped<ILaunchBL, LaunchBL>();
builder.Services.AddScoped<IRocketBL, RocketBL>();
builder.Services.AddScoped<ISpaceXAPI, SpaceXAPI>();
builder.Services.AddScoped<ILaunchCommandRepository, LaunchCommandRepository>();
builder.Services.AddScoped<IRocketCommandRepository, RocketCommandRepository>();
builder.Services.AddScoped<ILaunchQueryRepository, LaunchQueryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DapperConnectionProvider>();
    await context.Init();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
