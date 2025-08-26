using Newtonsoft.Json;

namespace HelpDesk.Api.Employee.Issues;


public enum ProblemImpact { Inconvenience, WorkStoppage }
public enum ProblemImpactRadius {  Personal, Customer}

public enum ProblemContactPreference {  Phone, Email }
public record SubmitIssueRequest
{
    public Guid SoftwareId { get; set; }
    public string Description { get; set; } = string.Empty;

    public ProblemImpact Impact { get; set; } = ProblemImpact.Inconvenience;
    public ProblemImpactRadius ImpactRadius { get; set; } = ProblemImpactRadius.Personal;
    public ProblemContactPreference ContactPreference { get; set; } = ProblemContactPreference.Phone;

    public Dictionary<ProblemContactPreference, string> ContactMechanisms { get; set; } = [];

}