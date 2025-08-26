

using System.Text.Json;
using HelpDesk.Api;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddOtel();

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        // options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper; // ImpactRadius -> IMPACT_RADIUS
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // No brainer.. Should be the default, imo. 
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Always;  // a little weirder, but I like it.
    });
builder.Services.AddOpenApi();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers(); 
app.Run();

