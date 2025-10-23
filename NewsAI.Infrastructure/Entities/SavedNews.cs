namespace NewsAI.Infrastructure.Entities;

public class SavedNews
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid NewsId { get; set; }
    
    //Relations

    public User User { get; set; } = null!;

    public virtual ICollection<News> News { get; set; } = new List<News>();
}