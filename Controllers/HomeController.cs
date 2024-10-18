using Microsoft.AspNetCore.Mvc;
using PROG6212_PART2_ST10396724.Models;
using System.Diagnostics;
using PROG6212_PART2_ST10396724.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PROG6212_PART2_ST10396724.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

       


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       

        public IActionResult AddUser()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

