namespace RequestResponseZeroMQ.Shared.DTOs
{
    public record UserSummaryDto
    {
        public int UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
    }
}
