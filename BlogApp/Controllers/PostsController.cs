using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;

        public PostsController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            //return View(_postRepository.Posts.ToList()); //interfacedeki iquerayble.Efpostadi iquereable
            return View
                (
                    new PostsViewModel
                    {
                        Posts = _postRepository.Posts.ToList(),
                        //Tags = _tagRepository.Tags.ToList(),
                    }
                );
        }

        public async Task<IActionResult> Details(String? url)
        {
            return View(await _postRepository.Posts.FirstOrDefaultAsync(p=>p.Url==url));
        }
    }
}
