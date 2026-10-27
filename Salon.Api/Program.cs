var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Вместо AddSwaggerGen() используем встроенный сервис
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Вместо UseSwagger() и UseSwaggerUI()
    app.MapOpenApi();
    // Для UI можно использовать встроенный интерфейс (если доступен в вашей версии)
    // или просто открывать JSON по адресу /openapi/v1.json
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Salon API v1");
});

app.Run();