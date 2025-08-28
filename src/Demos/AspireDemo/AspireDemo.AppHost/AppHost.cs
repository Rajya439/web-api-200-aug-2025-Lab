var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache"); // what containers, etc. you could add postgres, sql server, all sorts off stuff

var apiService = builder.AddProject<Projects.AspireDemo_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.AspireDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
