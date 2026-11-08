using Salon.Application.Services;
using Microsoft.EntityFrameworkCore;
using Salon.Contracts.Interfaces;
using Salon.Domain.Interfaces;
using Salon.Domain.Seeds;
using Salon.Infrastructure.EntityFrameworkCore.Context;
using Salon.Infrastructure.EntityFrameworkCore.Repositories;
using Salon.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


builder.AddMySqlDbContext<SalonDbContext>("salon-db");


builder.Services.AddScoped<IClientRepository, EfClientRepository>();
builder.Services.AddScoped<IMasterRepository, EfMasterRepository>();
builder.Services.AddScoped<IServiceRepository, EfServiceRepository>();
builder.Services.AddScoped<IAppointmentRepository, EfAppointmentRepository>();


builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

var app = builder.Build();

app.MapDefaultEndpoints();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SalonDbContext>();

    dbContext.Database.Migrate();

    
    if (!dbContext.Clients.Any())
    {
        var seed = new DataSeed();
        dbContext.Clients.AddRange(seed.Clients);
        dbContext.Masters.AddRange(seed.Masters);
        dbContext.Services.AddRange(seed.Services);
        dbContext.Appointments.AddRange(seed.Appointments);
        dbContext.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();