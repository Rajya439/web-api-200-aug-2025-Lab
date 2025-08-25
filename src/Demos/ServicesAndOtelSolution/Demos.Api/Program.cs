using Demos.Api.Home;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddTransient<HitCounter>(); // create a new one of these EVERY TIME any controller or service needs the HitCounter.
//builder.Services.AddScoped<HitCounter>(); // Create a new one of these for each request.
builder.Services.AddSingleton<ICountHits, PersistentHitCounter>(); // Just a single instance - shared across everything (ever request, every usage)
var connectionString = builder.Configuration.GetConnectionString("scratch") ?? throw new Exception("No Connection String");

builder.Services.AddMarten(c =>
{
    c.Connection(connectionString);

}).UseLightweightSessions(); // further configure this - this is a marten option, not all that interesting.
var app = builder.Build();

app.Use(async (context, next) =>
{
    // Everything that happens BEFORE you controller method is accessible here.
    Console.WriteLine("Got a Request");
   if( context.Request.Method == "GET")
    {
        Console.WriteLine("We have a get request");
        var hasAuth = context.Request.Headers.Authorization.FirstOrDefault();
        Console.WriteLine("Has an authorization header");

    }
    ;
     await next(context);
    // Everything AFTER your controller method is here.
    var mt = context.Response.Headers.ContentType.FirstOrDefault();
    Console.Write("Response content-type is", mt);
    Console.WriteLine("Sending a Response");
});
app.MapControllers(); // Look at the request for the method and the path, and direct to the right place.
app.Run();
