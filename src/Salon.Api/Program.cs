using Salon.Application.Services;
using Salon.Contracts.Interfaces;
using Salon.Domain.Interfaces;
using Salon.Domain.Seeds;
using Salon.Infrastructure.InMemory.Repositories;
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

builder.Services.AddSingleton<IClientRepository, InMemoryClientRepository>();
builder.Services.AddSingleton<IMasterRepository, InMemoryMasterRepository>();
builder.Services.AddSingleton<IServiceRepository, InMemoryServiceRepository>();
builder.Services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var seed = new DataSeed();

    var clientRepo = scope.ServiceProvider.GetRequiredService<IClientRepository>();
    foreach (var client in seed.Clients) await clientRepo.AddAsync(client);

    var masterRepo = scope.ServiceProvider.GetRequiredService<IMasterRepository>();
    foreach (var master in seed.Masters) await masterRepo.AddAsync(master);

    var serviceRepo = scope.ServiceProvider.GetRequiredService<IServiceRepository>();
    foreach (var service in seed.Services) await serviceRepo.AddAsync(service);

    var appointmentRepo = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();
    foreach (var appointment in seed.Appointments) await appointmentRepo.AddAsync(appointment);
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