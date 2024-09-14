namespace ITIBlog.Models.View
{
    public class AddLikeRequest
    {
        public Guid BlogPostId { get; set; }

        public Guid UserId { get; set; }
    }
}
