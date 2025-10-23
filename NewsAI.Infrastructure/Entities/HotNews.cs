namespace NewsAI.Infrastructure.Entities;

public class HotNews
{
    public Guid Id { get; set; } 
    
    public Guid NewsId { get; set; }
    
    public bool TopNews { get; set; } = false;

    public virtual ICollection<News> News { get; set; } = new List<News>();
}