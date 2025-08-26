
using System.Threading;
using System.Threading.Tasks;
using Marten;
using Marten.Linq.Parsing.Operators;

namespace HelpDesk.Api.Employee.Issues;

public class VipNotificationBackgroundWorker(
    ILogger<VipNotificationBackgroundWorker> logger,
    IServiceScopeFactory scopeFactory
   ) : BackgroundService
{



    private async Task CheckForUnhandledVips(CancellationToken token)
    {
       
        using var scope = scopeFactory.CreateScope(); // "defer" in golang
        using var session = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
        var problems = await session.Query<EmployeeProblemEntity>()
            .Where(p => p.Status == SubmittedIssueStatus.AwaitingTechAssignment && p.ReportedBy == "somevip")
            .ToListAsync(token);


    
        logger.LogInformation("There are {count} unhandled problems", problems.Count());

    }

   

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting VipNotification Background Worker");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(20));
            await CheckForUnhandledVips(stoppingToken);
        }
    }
}
