using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            var claims = User.Claims;

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
        public JsonResult AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment
            {
                PostId = PostId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? "")// nullsa boş string gönderebilir
                //User = new User { UserName = UserName, Image = "avatar.jpg" } username bilgisini elle kendimiz giriyorduk.
            };
            _commentRepository.CreateComment(entity);

            return Json(new
            {
                username,
                Text,
                entity.PublishedOn,
                avatar // entity.User.Image
            });

        }
    }
}
