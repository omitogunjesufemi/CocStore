using Api.Models;

namespace Api.Services.Results
{
    public record StatusRecord(StatusEnum Status, string? Message=null);

    public record OrderRecord(StatusEnum status,  Order? order = null, string? Message = null);
}
