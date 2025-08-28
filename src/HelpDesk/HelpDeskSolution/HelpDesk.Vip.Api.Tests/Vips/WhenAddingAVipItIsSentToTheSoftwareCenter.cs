

using Alba;
using HelpDesk.Vip.Api.Vips;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace HelpDesk.Vip.Api.Tests.Vips;

[Trait("Level", "UnitIntegration")]
public class WhenAddingAVipItIsSentToTheSoftwareCenter
{
    [Fact]
    public async Task NotificationIsSent()
    {
        var mockedSoftwareCenter = Substitute.For<ISendVipsToTheSoftwareCenter>();
        var host = await AlbaHost.For<Program>(c =>
        {
            c.ConfigureTestServices(sp =>
            {
                sp.AddScoped<ISendVipsToTheSoftwareCenter>(_ => mockedSoftwareCenter);
            });
        });

        
        // Post some data to VIPs
        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(new VipCreateModel
            {
                Sub = Guid.NewGuid().ToString(),
                Description = "Big Long Description that is valid"
            })
            .ToUrl("/vips");
            api.StatusCodeShouldBe(201);
        });

        // I Want to verify that the thing was called, and it was called with the right arguments and stuff.
        var postResponseBody = postResponse.ReadAsJson<VipDetailsModel>();

        Assert.NotNull(postResponseBody); // just making the compiler happy.

        // An "Assert - your test will fail if this doesn't work"
        await mockedSoftwareCenter.Received().SendSoftwareCenterNewVipAsync(postResponseBody);

    }
}
