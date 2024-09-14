namespace ITIBlog.Models.View
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool AdminRoleCheckBox { get; set; }
    }
}
