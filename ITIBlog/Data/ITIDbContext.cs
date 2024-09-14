using ITIBlog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ITIBlog.Data
{
    public class ITIDbContext : DbContext
    {
        public ITIDbContext(DbContextOptions<ITIDbContext> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
    }
}
