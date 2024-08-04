namespace BlogApp.Entity
{
    public class Tag
    {
        public int TagId { get; set; }
        public string? Text { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();// tag ve post çok'a çok ilişkisi barındırır
    }
}
