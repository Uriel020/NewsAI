namespace NewsAI.Core.Models.Auth.DTOs
{
    public class CreateUserDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? EmailAddress { get; set; }

        public string Password { get; set; } = null!;

        public int CardNumber { get; set; }

        public int PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}