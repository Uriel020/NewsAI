namespace NewsAI.Infrastructure.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    public bool IsActive { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}