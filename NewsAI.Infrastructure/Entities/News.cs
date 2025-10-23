namespace NewsAI.Infrastructure.Entities;

public class News
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;
    
    public DateTime PublishDate { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? UpdatedDate { get; set; }
}