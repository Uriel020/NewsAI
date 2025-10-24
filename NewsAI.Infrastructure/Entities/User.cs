namespace NewsAI.Infrastructure.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string? EmailAddress { get; set; }

    public string Password { get; set; } = null!;
    
    public int CardNumber {get; set;}
    
    public int PhoneNumber {get; set;}
    
    public DateTime DateOfBirth { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    //Relations

    public virtual ICollection<SavedNews> SavedNews { get; set; } = new List<SavedNews>();
}

