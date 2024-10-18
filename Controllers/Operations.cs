using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG6212_PART2_ST10396724.Data;
using PROG6212_PART2_ST10396724.Models;
using System.IO;
using System.Threading.Tasks;

namespace PROG6212_PART2_ST10396724.Controllers
{
    public class Operations : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<Operations> _logger;

        public Operations(AppDbContext context, ILogger<Operations> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Home/AddUser.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return View("~/Views/Home/ClaimTrack.cshtml");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View("~/Views/Home/AddUser.cshtml", lecturer);
            }
        }

        public IActionResult ClaimTrack()
        {
            return View("~/Views/Home/ClaimTrack.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(claim);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/AddUser.cshtml");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating claim");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the claim.");
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View("~/Views/Home/AddUser.cshtml");
            }
            return View("~/Views/Home/ClaimTrack.cshtml");
        }

        [HttpGet]
        public IActionResult AcademicManager()
        {
            return View("~/Views/Home/AcademicManagerView.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcademicManager(AcademicManager academicManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicManager);
                await _context.SaveChangesAsync();
                return RedirectToAction("ClaimTrack");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View("~/Views/Home/AcademicManagerView.cshtml", academicManager);
            }
        }

        [HttpGet]
        public IActionResult ProgCoOrdinator()
        {
            return View("~/Views/Home/ProgCoOrdinator.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProgCoOrdinator(ProgCoOrdinator progCoOrdinator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progCoOrdinator);
                await _context.SaveChangesAsync();
                return RedirectToAction("ClaimTrack");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View("~/Views/Home/ProgCoOrdinator.cshtml", progCoOrdinator);
            }
        }

        [HttpGet]
        public IActionResult ClaimApproval()
        {
            return View("~/Views/Home/ClaimApprovalView.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApprovalStatus(int claimID, string newApprovalStatus, int progCoOrdinatorID, int academicManagerID, int lecturerID)
        {
            if (ModelState.IsValid)
            {
                var claim = await _context.claim.FirstOrDefaultAsync(c => c.ClaimID == claimID);
                if (claim != null)
                {
                    claim.ApprovalStatus = newApprovalStatus;

                    var claimApproval = new ClaimApproval
                    {
                        ClaimID = claimID,
                        newApprovalStatus = newApprovalStatus,
                        ProgCoOrdinatorID = progCoOrdinatorID,
                        AcademicManagerID = academicManagerID,
                        LecturerID = lecturerID
                    };

                    _context.claimApproval.Add(claimApproval);
                    await _context.SaveChangesAsync();

                    return View("~/Views/Home/ClaimApprovalView.cshtml");
                }
                ModelState.AddModelError("", "Claim not found.");
            }
            return View("~/Views/Home/ClaimApprovalView.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> ClaimStatusView()
        {
            var pendingClaims = await _context.claim
                .Where(c => c.ApprovalStatus == "Pending")
                .ToListAsync();

            var pendingClaimApprovals = await _context.claimApproval
                .Where(ca => ca.newApprovalStatus == "Pending")
                .Include(ca => ca.claim)
                .ToListAsync();

            var approvedClaims = await _context.claimApproval
                .Where(ca => ca.newApprovalStatus == "Approved")
                .Include(ca => ca.claim)
                .ToListAsync();

            var deniedClaims = await _context.claimApproval
                .Where(ca => ca.newApprovalStatus == "Denied")
                .Include(ca => ca.claim)
                .ToListAsync();

            var model = new ClaimStatusViewModel
            {
                PendingClaims = pendingClaims,
                PendingClaimApprovals = pendingClaimApprovals,
                ApprovedClaims = approvedClaims,
                DeniedClaims = deniedClaims
            };

            return View("~/Views/Home/ClaimStatusView.cshtml", model);
        }

       
        

        [HttpGet]
        public IActionResult Upload()
        {
            return View("~/Views/Home/UploadFile.cshtml", new Document());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(Document model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        model.FileBinary = memoryStream.ToArray();
                    }

                    model.FileName = file.FileName;
                }
                else
                {
                    _logger.LogError("File is null or empty.");
                    ModelState.AddModelError(string.Empty, "File is required.");
                    return View("~/Views/Home/UploadFile.cshtml", model);
                }

                try
                {
                    _context.document.Add(model);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Document saved successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving document.");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the document.");
                    return View("~/Views/Home/UploadFile.cshtml", model);
                }

                return RedirectToAction("Index");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError(error.ErrorMessage);
            }

            return View("~/Views/Home/UploadFile.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var documents = await _context.document.ToListAsync();
            return View("~/Views/Home/Index.cshtml", documents);
        }

        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var document = await _context.document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            return File(document.FileBinary, "application/octet-stream", document.FileName);
        }
    }
}