namespace NewsAI.Core.Models.News;

public class CreateNewsDto
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Url { get; set; } = null!;

    public Guid CategoryId { get; set; }
}