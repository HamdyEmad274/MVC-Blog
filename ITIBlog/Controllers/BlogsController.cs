using ITIBlog.Models.Domain;
using ITIBlog.Models.View;
using ITIBlog.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITIBlog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogsController(IBlogPostRepository blogPostRepository
            , IBlogPostLikeRepository blogPostLikeRepository 
            , SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, 
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogPostDetailsViewModel = new BlogDetailsViewModel();
            
            if (blogPost != null)
            {
                //Get total likes
                var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);
                if (signInManager.IsSignedIn(User))
                {
                    //Get Likes for this blog ny this user 
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);
                    var userId = userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }
                //Get Comments For this blog
                var blogCommentsDomainModel = await blogPostCommentRepository
                    .GetCommentsByBlogIdAsync(blogPost.Id);

                var blogCommentsForView = new List<BlogComment>();

                foreach (var blogComment in blogCommentsDomainModel)
                {
                    blogCommentsForView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        UserName = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
                }
                //Mapping domain model to view model
                blogPostDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Tags = blogPost.Tags,
                    Visible = blogPost.Visible,
                    Heading = blogPost.Heading,
                    UrlHandle = blogPost.UrlHandle,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = blogCommentsForView
                };
            }

            return View(blogPostDetailsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogPostDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogPostDetailsViewModel.Id,
                    Description = blogPostDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await blogPostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Blogs",
                    new { urlHandle = blogPostDetailsViewModel.UrlHandle });
            }
            return View();
            
        }
    }
}
