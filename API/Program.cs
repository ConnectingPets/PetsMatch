using API.Hubs;
using API.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.ConfigurateDbContext(builder.Configuration);
builder.Services.ConfigurateIdentity(builder.Configuration);
builder.Services.ConfigurateCors(builder.Configuration);
builder.Services.ConfigurateServices();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors("ReactPolicy");

app.MapHub<ChatHub>("/chat");

app.UseRouting();

await app.RunAsync();