using EDoc2.FAQ.Core.Domain.Models.CommentAggregate;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore
{
    public class CommentRepository: ICommentRepository
    {
        private readonly CommunityContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CommentRepository(CommunityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Comment Add(Comment entity)
        {
            return entity.Id != default(Guid) ? entity : _context.Comments.Add(entity).Entity;
        }

        public void Delete(Comment entity)
        {
            _context.Comments.Remove(entity);
        }

        public Comment Update(Comment entity)
        {
            return _context.Comments.Update(entity).Entity;
        }

        public async Task<Comment> FindAsync(Guid key)
        {
            return await _context.Comments.FindAsync(key);
        }
    }
}
