namespace Kairos.Infrastructure.Persistence;

public sealed class StoredActivityAuditEvent
{
    public Guid Id { get; set; }
    public required string OwnerSubject { get; set; }
    public Guid ActivityId { get; set; }
    public required string Action { get; set; }
    public DateTimeOffset OccurredAtUtc { get; set; }
    public required string Details { get; set; }
}
