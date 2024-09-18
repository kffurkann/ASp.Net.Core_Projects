using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;
        private ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, ITagRepository tagRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
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

            var posts = _postRepository.Posts.Where(i => i.IsActive);//_postRepository.Posts;//toList dersem veritabanından alırım oyüzden filtrelemeye devam

            if (!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(String? url)
        {
            return View(await _postRepository.Posts
                        .Include(x => x.User)//postun
                        .Include(x => x.Tags)
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.User)//yorumun
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

        [Authorize]//sayfaya url ile ulaşmayı engelliyor
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _postRepository.CreatePost(
                    new Post
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Url = model.Url,
                        UserId = int.Parse(userId ?? ""),
                        PublishedOn = DateTime.Now,
                        Image = "1.jpg",
                        IsActive = false
                    }
                );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role))
            {
                posts = posts.Where(i => i.UserId == userId);
            }

            return View(await posts.ToListAsync());
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _postRepository.Posts.Include(i => i.Tags).FirstOrDefault(i => i.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            ViewBag.Tags = _tagRepository.Tags.ToList();// veritabanına atanmış tüm etiketler

            return View(new PostCreateViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Tags = post.Tags
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model, int[] tagIds)
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = new Post
                {
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url
                };

                if (User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entityToUpdate.IsActive = model.IsActive;
                }

                _postRepository.EditPost(entityToUpdate, tagIds);
                return RedirectToAction("List");
            }
            ViewBag.Tags = _tagRepository.Tags.ToList();//hata varsa mevcut tagleri tekrar yüklesin
            return View(model);
        }
    }
}
