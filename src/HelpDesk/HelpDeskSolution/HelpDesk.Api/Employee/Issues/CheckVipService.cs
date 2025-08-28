
using SharedContracts;

namespace HelpDesk.Api.Employee.Issues;


// This is a SERVICE that owns ALL communication with the HelpDesk.Vip.Api 
public class CheckVipService(HttpClient client)
{
    public async Task<HelpDeskVipResponse> GetCurrentVipsAsync(CancellationToken token)
    {
        var response = await client.GetAsync("/help-desk/vips");

        response.EnsureSuccessStatusCode(); // more in a minute 200-299 

        var vips = await response.Content.ReadFromJsonAsync<HelpDeskVipResponse>();
        if (vips is null)
        {
            return new HelpDeskVipResponse([], 0);
        }
        else
        {
            return vips;
        }

    }
}