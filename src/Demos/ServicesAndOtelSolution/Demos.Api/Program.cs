using Demos.Api.Home;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddTransient<HitCounter>(); // create a new one of these EVERY TIME any controller or service needs the HitCounter.
//builder.Services.AddScoped<HitCounter>(); // Create a new one of these for each request.
builder.Services.AddKeyedSingleton<ICountHits, PersistentHitCounter>("persistent"); // Just a single instance - shared across everything (ever request, every usage)
builder.Services.AddKeyedSingleton<ICountHits, HitCounter>("non");
var connectionString = builder.Configuration.GetConnectionString("scratch") ?? throw new Exception("No Connection String");

builder.Services.AddMarten(c =>
{
    c.Connection(connectionString);

}).UseLightweightSessions(); // further configure this - this is a marten option, not all that interesting.
var app = builder.Build();

app.MapControllers();
app.Run();
