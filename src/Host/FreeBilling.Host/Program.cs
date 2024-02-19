var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.FreeBilling_Api>("Api");

builder.AddNpmApp("UI", @"../freebilling.ui/")
  .WithReference(api)
  .WithHttpEndpoint(3000, env: "PORT")
  .AsDockerfileInManifest();

builder.Build().Run();
