using Microsoft.AspNetCore.Identity;

namespace ITIBlog.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>>GetAll();
    }
}
