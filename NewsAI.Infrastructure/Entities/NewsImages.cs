namespace NewsAI.Infrastructure.Entities;

public class NewsImages
{
    public Guid Id { get; set; }

    public string Url { get; set; } = null!;

    public bool IsPrimary { get; set; } = false;
    
    public Guid NewsId { get; set; }

    public virtual News News { get; set; } = null!;
}