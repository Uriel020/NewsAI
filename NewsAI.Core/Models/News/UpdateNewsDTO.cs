namespace NewsAI.Core.Models.News;

public class UpdateNewsDTO
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public int? Views { get; set; }

    public bool? HotNews { get; set; }
        
    public Guid CategoryId { get; set; }
}