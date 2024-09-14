
using ITIBlog.Data;
using ITIBlog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ITIBlog.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly ITIDbContext dbContext;

        public BlogPostLikeRepository(ITIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await dbContext.BlogPostLikes.AddAsync(blogPostLike);
            await dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await dbContext.BlogPostLikes.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await dbContext.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
