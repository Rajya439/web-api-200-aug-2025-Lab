
namespace HelpDesk.Vip.Api.Vips
{
    public interface ISendVipsToTheSoftwareCenter
    {
        Task SendSoftwareCenterNewVipAsync(VipDetailsModel vip, CancellationToken token = default);
    }
}