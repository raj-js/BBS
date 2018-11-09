using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Models.CommentAggregate
{
    public interface ICommentRepository : IRepository<Comment, Guid>
    {
    }
}
