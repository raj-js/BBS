using System;

namespace EDoc2.FAQ.Core.Application.DtoBase
{
    public interface IDateRangeReq
    {
        DateTime? BeginDate { get; }

        DateTime? EndDate { get; }
    }
}
