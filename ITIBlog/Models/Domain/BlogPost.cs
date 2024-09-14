namespace ITIBlog.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; } // Global Unique Identifier
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<BlogPostLike> Likes { get; set; }
        public virtual ICollection<BlogPostComment> Comments { get; set; }
    }
}
