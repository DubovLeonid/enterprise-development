var builder = DistributedApplication.CreateBuilder(args);


var mysql = builder.AddMySql("mysql")
    .WithLifetime(ContainerLifetime.Persistent); 


var salonDb = mysql.AddDatabase("salon-db");


builder.AddProject<Projects.Salon_Api>("salon-api")
    .WithReference(salonDb)
    .WaitFor(salonDb);

builder.Build().Run();