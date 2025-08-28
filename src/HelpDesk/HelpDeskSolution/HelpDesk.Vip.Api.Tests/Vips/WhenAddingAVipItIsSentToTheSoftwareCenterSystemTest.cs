

using System.Text.Json;
using Alba;
using HelpDesk.Vip.Api.Vips;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace HelpDesk.Vip.Api.Tests.Vips;

[Trait("Level", "System")]
public class WhenAddingAVipItIsSentToTheSoftwareCenterSystemText : IAsyncLifetime
{

    private IAlbaHost _host = null!;
    private WireMockServer _server = null!;
    public async Task InitializeAsync()
    {
        _server = WireMockServer.Start();
        _host = await AlbaHost.For<Program>(h =>
        {
            h.UseSetting("ConnectionStrings:software-center", _server.Urls[0]);
        });
    }
    public async Task DisposeAsync()
    {
        _server?.Dispose();
        await _host.DisposeAsync();
    }

 

    [Fact]
    public async Task NotificationIsSent()
    {
        //var mockedSoftwareCenter = Substitute.For<ISendVipsToTheSoftwareCenter>();

        _server
            .Given(Request.Create()
                .WithPath("/help-desk/vips")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(200));

        
        // Post some data to VIPs
        var postResponse = await _host.Scenario(api =>
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
        //awa
        //it mockedSoftwareCenter.Received().SendSoftwareCenterNewVipAsync(postResponseBody);
        var sentJson = _server.LogEntries[0].RequestMessage.Body;
        Assert.NotNull(sentJson);
      var sentMessage = JsonSerializer.Deserialize<VipDetailsModel>(sentJson, JsonSerializerOptions.Web);
        Assert.Equal(postResponseBody, sentMessage);
    }

    [Fact]
    public async Task WhatIfThatServerIsDown()
    {
        _server
          .Given(Request.Create()
              .WithPath("/help-desk/vips")
              .UsingPost())
          .RespondWith(Response.Create().WithStatusCode(500));


        // Post some data to VIPs
        var postResponse = await _host.Scenario(api =>
        {
            api.Post.Json(new VipCreateModel
            {
                Sub = Guid.NewGuid().ToString(),
                Description = "Big Long Description that is valid"
            })
            .ToUrl("/vips");
            api.StatusCodeShouldBe(201);
        });


        // I need to check to see if in this test, it wrote it to that database table, or whatever..

    
    }
}
