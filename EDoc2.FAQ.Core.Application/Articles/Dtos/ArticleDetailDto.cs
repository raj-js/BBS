using System;

namespace EDoc2.FAQ.Core.Application.Articles.Dtos
{
    public class ArticleDetailDto : EntityDto<Guid>
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string Keywords { get; set; }

        public int StateId { get; set; }

        public int TypeId { get; set; }

        public bool CanComment { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
