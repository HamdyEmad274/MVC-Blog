using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIBlog.Models.View
{
    public class EditBlogPostRequest
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
        public IEnumerable<SelectListItem> Tags { get; set; }
        //Collect Tags
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
