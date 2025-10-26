namespace NewsAI.Core.Entities;

public class News
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int Views { get; set; }

    public bool HotNew { get; set; } = false;

    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public Guid? CategoryId { get; set; }
    
    //Relations    

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<SavedNews> SavedByUser { get; set; } = new List<SavedNews>();
    
    public virtual ICollection<NewsImages> NewsImages { get; set; } = new List<NewsImages>();
}