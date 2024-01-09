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
builder.Services.AddCloudinary(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("ReactPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.SeedRoles();

await app.RunAsync();