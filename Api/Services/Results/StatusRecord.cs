namespace Api.Services.Results
{
    public record StatusRecord(StatusEnum Status, string? Message=null);
}
