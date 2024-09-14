using System.ComponentModel.DataAnnotations;

namespace ITIBlog.Models.View
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Password must be at least 6 characters")]
        public string Password { get; set; }
    }
}
