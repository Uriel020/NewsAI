namespace NewsAI.Core.Models.Auth.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? EmailAddress { get; set; }

        public int CardNumber { get; set; }

        public int PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}