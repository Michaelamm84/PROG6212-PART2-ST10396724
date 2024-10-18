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

       

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.claims.Add(claim);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ClaimTrack));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating claim");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the claim.");
                }
            }
            return View("ClaimTrack", claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTest(test testModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.tests.Add(testModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Privacy", "Home");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving test model");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }
            return View("Privacy", testModel);
        }
       */

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

