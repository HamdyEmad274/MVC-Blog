using ITIBlog.Data;
using ITIBlog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ITIBlog.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly ITIDbContext dbContext;

        public BlogPostCommentRepository(ITIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await dbContext.BlogPostComments.AddAsync(blogPostComment);
            await dbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
           return await dbContext.BlogPostComments
                .Where(x => x.BlogPostId == blogPostId).ToListAsync();

        }
    }
}
