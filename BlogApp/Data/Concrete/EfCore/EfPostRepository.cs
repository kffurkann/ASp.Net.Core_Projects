using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;
        public EfPostRepository(BlogContext context)
        {
            _context=context;
        }

        public IQueryable<Post> Posts => _context.Posts;
        //Posts özelliği, BlogContext içindeki Posts DBSet'ini döner. Bu, Post nesnelerini sorgulamak için kullanılır.
        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}
