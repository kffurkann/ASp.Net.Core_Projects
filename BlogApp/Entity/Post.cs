﻿namespace BlogApp.Entity
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!; //Bir post bir usera ait olacak --- bire çok ilişkisi

        public List<Tag> Tags { get; set;} = new List<Tag>(); //bir postun birden fazla tag'i olabilir
        public List<Comment> Comments { get; set; } = new List<Comment>(); //bir postun birden fazla comment'i olabilir
    }
}
