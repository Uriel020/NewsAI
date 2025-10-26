namespace NewsAI.Core.Models.News
{
    public class NewsDto
    {
        public Guid Id { get; set; }
    
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Url { get; set; } = null!;

        public int Views { get; set; }

        public bool HotNews { get; set; }
        
        public Guid? CategoryId { get; set; }
    }
}