using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyCource.Models;
using UdemyCource.Services;

namespace UdemyCource.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IContentPageDetailsServices _contentPageDetailsService;
        public HomeController(IContentPageDetailsServices contentPageDetailsService)
        {
            _contentPageDetailsService= contentPageDetailsService;
        }

     //   [AllowAnonymous]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult SecureContent()
        {
            var model = _contentPageDetailsService.PostContents;
            return View(model);
        }

        [Authorize]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(PostContent postContent)
        {
            _contentPageDetailsService.AddContent(postContent);
            return RedirectToAction("SecureContent");
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            _contentPageDetailsService.RemoveContent(id);
            return RedirectToAction("SecureContent");
        }
    }
}
