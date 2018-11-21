using System;
using System.Collections.Generic;

namespace EDoc2.FAQ.Core.Application.ServiceBase
{
    public class PagingDto<T>
    {
        public int TotalCount { get; set; }

        public List<T> Dtos { get; set; }

        public PagingDto(int totalCount, List<T> dtos)
        {
            TotalCount = totalCount;
            Dtos = dtos ?? new List<T>();
        }
    }
}
