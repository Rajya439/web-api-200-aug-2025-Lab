namespace HelpDesk.Vip.Api.Vips;

public class SoftwareCenterApi(HttpClient client) : ISendVipsToTheSoftwareCenter
{
    public async Task SendSoftwareCenterNewVipAsync(VipDetailsModel vip, CancellationToken token = default)
    {
        var response = await client.PostAsJsonAsync("/help-desk/vips", vip, token);
        response.EnsureSuccessStatusCode();
    }

    // a bunch of other methods we might need to call that have to with the software center
}
