namespace NewsAI.Infrastructure.Entities;

public class SavedNews
{
    public Guid UserId { get; set; }
    
    public Guid NewsId { get; set; }
    
    public DateTime SavedAt { get; set;}
    //Relations

    public virtual User User { get; set; } = null!;

    public virtual News News { get; set; } = null!;
}