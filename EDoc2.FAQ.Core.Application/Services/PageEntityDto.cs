using System.Collections.Generic;

namespace EDoc2.FAQ.Core.Application
{
    public class PageEntityDto<TPrimaryKey>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Totral { get; set; }

        public List<EntityDto<TPrimaryKey>> EntityDtos { get; set; }
    }
}
