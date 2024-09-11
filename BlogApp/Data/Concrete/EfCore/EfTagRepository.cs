using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfTagRepository : ITagRepository
    {
        private BlogContext _context;
        public EfTagRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> Tags => _context.Tags;
        //Posts özelliği, BlogContext içindeki Posts DBSet'ini döner. Bu, Post nesnelerini sorgulamak için kullanılır.

        public void CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
    }

}
