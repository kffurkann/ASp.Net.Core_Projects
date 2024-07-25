using Microsoft.AspNetCore.Mvc;
using MeetingApp.Models;

namespace MeetingApp.Controllers
{
    public class MeetingController : Controller
    {
        /*
        public string Index()
        {
            return "meeting/index";
        }
        */
      
        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Apply(UserInfo model)
        {
            if(ModelState.IsValid) 
            { 
                Repository.CreateUser(model);
                ViewBag.UserCount = Repository.Users.Where(info => info.WillAttend == true).Count();
                return View("Thanks", model);
            }
            else 
            {
                return View(model);//form tekrar gönderilir, yanlış yaptığı verileri tekrar görmüş olur.
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(Repository.Users);
        }

        // meeting/details/2 ->program.cs
        public IActionResult Details(int id)
        {
            return View(Repository.GetById(id));
        }
    }
}
