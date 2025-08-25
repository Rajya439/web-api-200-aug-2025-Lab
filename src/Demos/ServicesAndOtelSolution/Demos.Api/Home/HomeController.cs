

namespace Demos.Api.Home;

public class HomeController : ControllerBase
{
    private ICountHits _hitCounter;

    public HomeController(ICountHits hitCounter)
    {
        _hitCounter = hitCounter;
    }

    [HttpGet("/")]
    public async Task<Ok<HomePageResponse>> GetHome(CancellationToken token)
    {
        
       
        return TypedResults.Ok(new HomePageResponse(await _hitCounter.GetHitCount(token)));
    }
}

public record HomePageResponse(int HitCount);
