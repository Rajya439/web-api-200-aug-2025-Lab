
using Alba;
using HelpDesk.Vip.Api.Vips;
using JasperFx.Core;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace HelpDesk.Vip.Api.Tests.Vips;

public class AddingAVip
{
    [Fact()]
    public async Task CanAddAVip()
    {
        // "gray box testing" - start up the Api (Vip API)
        var host = await AlbaHost.For<Program>(
            config =>
            {
                config.ConfigureTestServices(sp =>
                {
                    sp.AddScoped<ISendVipsToTheSoftwareCenter>(sp =>
                    {
                        return Substitute.For<ISendVipsToTheSoftwareCenter>();
                    });
                   
                });
            }
            );

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

        var postReturnBody = postResponse.ReadAsJson<VipDetailsModel>();
        Assert.NotNull(postReturnBody);

        var location = postResponse.Context.Response.Headers.Location[0];
        Assert.NotNull(location);

        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location);
            api.StatusCodeShouldBeOk();
        });

        var getResponseBody = getResponse.ReadAsJson<VipDetailsModel>();
        Assert.NotNull(getResponseBody);

        Assert.Equal(postReturnBody, getResponseBody);
    }
}
