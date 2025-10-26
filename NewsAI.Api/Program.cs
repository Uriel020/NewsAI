
using NewsAI.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureDb(builder.Configuration)
    .ConfigureInjections()
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
