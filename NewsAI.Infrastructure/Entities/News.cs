namespace NewsAI.Infrastructure.Entities;

public class News
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public Guid CategoryId { get; set; }
    
    //Relations

    public virtual HotNews HotNews { get; set; } = null!;
    
    public virtual SavedNews SavedNews { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}