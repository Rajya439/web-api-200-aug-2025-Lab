

namespace Demos.Api.Home;

public class HomeController : ControllerBase
{


    [HttpGet("/")]
    public async Task<Ok<HomePageResponse>> GetHome(
        [FromKeyedServices("persistent")] ICountHits hitCounter,
        CancellationToken token)
    {
       
        return TypedResults.Ok(new HomePageResponse(await hitCounter.GetHitCount(token)));
    }

    [HttpPost("/")]
    public async Task<Ok<HomePageResponse>> PostSomething(
         [FromKeyedServices("non")] ICountHits hitCounter,
        CancellationToken token)
    {
        return TypedResults.Ok(new HomePageResponse(await hitCounter.GetHitCount(token)));
    }
}

public record HomePageResponse(int HitCount);
