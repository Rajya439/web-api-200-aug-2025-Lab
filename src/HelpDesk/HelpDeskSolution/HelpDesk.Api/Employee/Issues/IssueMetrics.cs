using System.Diagnostics.Metrics;

namespace HelpDesk.Api.Employee.Issues;

public class IssueMetrics
{
    private readonly Counter<int> _problemsCreated;

    public IssueMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("HelpDesk.Api");
        _problemsCreated = meter.CreateCounter<int>("helpdesk.api.employees.problems_created");
    }
    public void ProblemCreated()
    {
        _problemsCreated.Add(1);
    }
}

