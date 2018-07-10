using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Wynlist.Data;
using Wynlist.Data.Entities;
using Wynlist.Services;
using Wynlist.ViewModels;
using WYNlist.Controllers;
using WYNlist.ViewModels;

namespace Wynlist.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IWynlistRespository _repository;
        private readonly ILogger<AccountController> _logger;

        public AppController(IMailService mailService, 
            IWynlistRespository repository, 
            ILogger<AccountController> logger)
        {
            _repository = repository;
            this._mailService = mailService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            //ViewBag.Title = "Contact Us";

            //throw new InvalidOperationException("Bad things happened");

            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //send email
                _mailService.SendMessage("gabeheter@hotmail.com", model.Subject, model.Message);
                ViewBag.UserMessage = "Mail sent";
                ModelState.Clear();
            }

            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            //ViewBag.Title = "About Us";

            return View();
        }

        [Authorize]
        public IActionResult List()
        {
            // var results = _context.List
            //     .OrderBy(p => p.ListTypeID)
            //     .ToList();

            //OR with LINQ

            //var username = User.Identity.Name;
            var results = _repository.GetAllLists(User.Identity.Name);

            return View(results);
        }

        [Authorize]
        public IActionResult ListType()
        {
            // var results = _context.List
            //     .OrderBy(p => p.ListTypeID)
            //     .ToList();

            //OR with LINQ

            var results = _repository.GetAllListTypes();

            return View(results);
        }

        [Authorize]
        public IActionResult Recipe()
        {
            // var results = _context.List
            //     .OrderBy(p => p.ListTypeID)
            //     .ToList();

            //OR with LINQ
            //var username = User.Identity.Name;
            var results = _repository.GetAllRecipes(User.Identity.Name);

            return View(results);
        }

        [Authorize]
        public IActionResult RecipeDetails(int id)
        {
            //var username = User.Identity.Name;
            var results = _repository.GetRecipeById(User.Identity.Name, id);

            return View(results);
        }

        [Authorize]
        [HttpGet("RecipeEdit")]
        public IActionResult RecipeEdit(int id)
        {
            //var username = User.Identity.Name;
            var results = _repository.GetRecipeById(User.Identity.Name, id);

            return View(results);
        }

        [Authorize]
        public IActionResult RecipeTypes()
        {
            // var results = _context.List
            //     .OrderBy(p => p.ListTypeID)
            //     .ToList();

            //OR with LINQ

            var results = _repository.GetAllRecipeTypes();

            return View(results);
        }
    }
}