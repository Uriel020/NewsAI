namespace NewsAI.Core.Models.News;

public class UpdateNewsDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public int? Views { get; set; }

    public bool? HotNews { get; set; }
        
    public Guid CategoryId { get; set; }
}