using System.Collections.Generic;

namespace PzDnesPublicator.App.DTOs
{
    public class NewsDTO
    {
        public List<ArticleDTO> Articles { get; set; }
    }

    public class ArticleDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ArticleNumber { get; set; }
    }
}
