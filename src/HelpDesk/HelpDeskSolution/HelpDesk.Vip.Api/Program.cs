using HelpDesk.Vip.Api.Vips;
using Marten;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton(_ => TimeProvider.System);
builder.Services.AddOpenApi();
builder.Services.AddMarten(opts =>
{
    var connectionString = builder.Configuration.GetConnectionString("vips") ??
                           throw new Exception("Missing connection string");
    opts.Connection(connectionString);
}).UseLightweightSessions();

// This the "Thing" that handles all software center stuff.
builder.Services.AddHttpClient<SoftwareCenterApi>(client =>
{
    var uri = builder.Configuration.GetConnectionString("software-center") ?? throw new Exception("Need a uri for the software center");
    client.BaseAddress = new Uri(uri);
});

// If someone wants to send VIps to the SC, use the SoftwareCenterApi
builder.Services.AddScoped<ISendVipsToTheSoftwareCenter>(sp =>
{
    return sp.GetRequiredService<SoftwareCenterApi>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();
app.Run(); // <- here is when it is running! 

public partial class Program;