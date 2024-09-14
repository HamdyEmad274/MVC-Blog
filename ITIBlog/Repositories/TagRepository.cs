using ITIBlog.Data;
using ITIBlog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ITIBlog.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ITIDbContext dbContext;

        public TagRepository(ITIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                dbContext.Tags.Remove(existingTag);
                await dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
           return await dbContext.Tags.FindAsync(id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
