
using NewsAI.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDb(builder.Configuration)
    .ConfigureInjections()
    .AddOpenApi()
    .AddControllers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.MapControllers();
app.UseSwagger();
app.Run();
