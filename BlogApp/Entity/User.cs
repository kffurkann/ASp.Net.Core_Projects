namespace BlogApp.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Image { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();//bir user'ın birden fazla postu olabilir
        public List<Comment> Comments { get; set; } = new List<Comment>();//bir user'ın birden fazla comment'i olabilir
    }
}
