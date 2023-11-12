using System.Reflection;

using MediatR;

using API.Infrastructure;
using Application.DTOs;
using static Application.Animal.AddAnimal;
using static Application.Animal.AllAnimal;
using static Application.Animal.ShowAnimalToAdd;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurateDbContext(builder.Configuration);
builder.Services.ConfigurateServices();

string reactBaseUrl = builder.Configuration.GetValue<string>("ReactApp:BaseUrl") ?? 
    throw new InvalidOperationException("The react base url is not found.");

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", builder =>
    {
        builder.WithOrigins(reactBaseUrl)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers();
builder.Services.AddScoped<IRequestHandler<AddAnimalCommand, Unit>, AddAnimalCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AllAnimalQuery, IEnumerable<AnimalDto>>, AllAnimalQueryHandler>();
builder.Services.AddScoped<IRequestHandler<ShowAnimalToAddQuery, ShowAnimalToAddDto>, ShowAnimalToAddQueryHandler>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseCors("ReactPolicy");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
