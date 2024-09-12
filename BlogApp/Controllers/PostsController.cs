using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        //private ITagRepository _tagRepository;
        private ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)//, ITagRepository tagRepository
        {
            _postRepository = postRepository;
            //_tagRepository = tagRepository;
            _commentRepository = commentRepository;
        }
        /*
        public IActionResult Index()
        {

            //return View(_postRepository.Posts.ToList()); //interfacedeki iquerayble.Efpostadi iquereable

            return View(
                new PostsViewModel
                {
                    Posts = _postRepository.Posts.ToList()
                    //Tags = _tagRepository.Tags.ToList(),
                }
            );
        }
        */
        public async Task<IActionResult> Index(string tag)//program.cs de tag olduğu için
        {
            //return View(_postRepository.Posts.ToList()); //interfacedeki iquerayble.Efpostadi iquereable

            var posts = _postRepository.Posts;//toList dersem veritabanından alırım oyüzden filtrelemeye devam

            if (!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(String? url)
        {
            return View(await _postRepository.Posts
                        .Include(x => x.Tags)
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.User)
                        .FirstOrDefaultAsync(p => p.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string UserName, string Text)
        {
            var entity = new Comment
            {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = new User { UserName = UserName, Image = "avatar.jpg" }
            };
            _commentRepository.CreateComment(entity);

            return Json(new
            {
                UserName,
                Text,
                entity.PublishedOn,
                entity.User.Image
            });

        }
    }
}
