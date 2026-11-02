var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Salon_Api>("salon-api");

builder.Build().Run();
