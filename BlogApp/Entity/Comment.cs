namespace BlogApp.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }
        public int PostId { get; set; } // Foreign key
        public Post Post { get; set; } = null!;// Navigation property // bir comment bir posta ait olacak
        public int UserId { get; set; }
        public User User { get; set; } = null!; // bir comment bir usera ait olacak
    }
}
