using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG6212_PART2_ST10396724.Data;
using PROG6212_PART2_ST10396724.Migrations;
using PROG6212_PART2_ST10396724.Models;
using System.IO;
using System.Threading.Tasks;

/*
 * Author: Michael AMM.
 * ST10396724
 * ProgPOEPart3
 * Group 1
 */


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
        //-----------------------------------------------------------------------
        //-----------------------------------------------------------------------
        [HttpPost]
        public IActionResult EditLecturer(int LecturerID)
        {
            var lecturer = _context.lecturer.FirstOrDefault(l => l.LecturerID == LecturerID);
            if (lecturer == null)
            {
                return NotFound("Lecturer not found.");
            }

            return View("~/Views/Home/EditLecturer.cshtml", lecturer);
        }


        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------

        public IActionResult HRManagementView()
        {
            return View("~/Views/Home/HRManagementView.cshtml");
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        [HttpGet]
        public IActionResult HRManagement()
        {
            // Fetch lecturers and claims from the database
            var lecturers = _context.lecturer.ToList();
            var claims = _context.claim.Include(c => c.lecturer).ToList();

            // Pass data to the view using ViewBag
            ViewBag.Lecturers = lecturers;
            ViewBag.Claims = claims;

            return View(); // No need for a ViewModel
        }

        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------



        // Generate Invoice for a Claim
        [HttpPost]
        public IActionResult GenerateInvoice(int claimID)
        {
            var claim = _context.claim
                .Include(c => c.lecturer)
                .FirstOrDefault(c => c.ClaimID == claimID);

            if (claim == null)
                return NotFound("Claim not found.");

            // Invoice generation logic here (use iTextSharp, Crystal Reports, or similar)
            var invoice = GenerateInvoicePDF(claim);

            return File(invoice, "application/pdf", $"Invoice_Claim_{claimID}.pdf");
        }

        //------------------------------------------------------
        //------------------------------------------------------


        private byte[] GenerateInvoicePDF(Claim claim)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Create PDF writer
                var writer = new iText.Kernel.Pdf.PdfWriter(memoryStream);
                var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                var document = new iText.Layout.Document(pdf);

                // Add title
                document.Add(new iText.Layout.Element.Paragraph("Invoice")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20)
                    );

                // Add claim details
                document.Add(new iText.Layout.Element.Paragraph($"Claim ID: {claim.ClaimID}")
                    .SetFontSize(12));
                document.Add(new iText.Layout.Element.Paragraph($"Lecturer Name: {claim.lecturer?.LecturerName ?? "N/A"}")
                    .SetFontSize(12));
                document.Add(new iText.Layout.Element.Paragraph($"Hours Worked: {claim.hoursWorked}")
                    .SetFontSize(12));
                document.Add(new iText.Layout.Element.Paragraph($"Hourly Pay: {claim.hourlyPay:C}")
                    .SetFontSize(12));
                document.Add(new iText.Layout.Element.Paragraph($"Total Contract Value: {claim.ContractValue:C}")
                    .SetFontSize(12));

                // Add footer
                document.Add(new iText.Layout.Element.Paragraph("Thank you for your work!")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(10)
                    );

                // Close the document
                document.Close();

                // Return PDF as a byte array
                return memoryStream.ToArray();
            }
        }
        //------------------------------------------------------
        //------------------------------------------------------

        // Save Lecturer Data
        [HttpPost]
        public IActionResult UpdateLecturer(Lecturer updatedLecturer)
        {
            var lecturer = _context.lecturer.FirstOrDefault(l => l.LecturerID == updatedLecturer.LecturerID);
            if (lecturer == null)
            {
                return NotFound("Lecturer not found.");
            }

            lecturer.LecturerName = updatedLecturer.LecturerName;
            lecturer.Email = updatedLecturer.Email;

            _context.SaveChanges();

            return View("~/Views/Home/AddUser.cshtml");
        }









        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        [HttpGet]
        public IActionResult GetContractDetails(int claimID)
        {
            // Check if the Claim ID is valid
            if (claimID <= 0)
            {
                return Json(new { success = false, message = "Invalid Claim ID." });
            }

            // Fetch the claim from the database (using Entity Framework)
            var claim = _context.claim
                .Where(c => c.ClaimID == claimID)
                .Select(c => new
                {
                    c.ClaimID,
                    c.hoursWorked,
                    c.hourlyPay,
                    ContractValue = c.hoursWorked * c.hourlyPay
                })
                .FirstOrDefault();

            // If claim is not found, return an error response
            if (claim == null)
            {
                return Json(new { success = false, message = "Claim not found." });
            }

            // Return the claim details
            return Json(new
            {
                success = true,
                data = new
                {
                    claim.ClaimID,
                    claim.hoursWorked,
                    claim.hourlyPay,
                    claim.ContractValue
                }
            });
        }



        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Home/AddUser.cshtml");
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public async Task<IActionResult> ValidateClaim(int claimId)
        {
            var claim = await _context.claim.FindAsync(claimId);
            if (claim == null)
            {
                ViewData["ValidationErrors"] = new List<string> { "Claim not found." };
                return View();
            }

            var validationErrors = new List<string>();

            if (claim.hoursWorked < 50)
            {
                validationErrors.Add("Lecturer must have worked a minimum of 50 hours.");
            }

            if (claim.hoursWorked > 500)
            {
                validationErrors.Add("Lecturer cannot have worked more than 500 hours.");
            }

            if (claim.hourlyPay > 30)
            {
                validationErrors.Add("Hourly pay cannot be more than thirty rand.");
            }

            ViewData["ValidationErrors"] = validationErrors.Any() ? validationErrors : null;
            return View();
        }



        //--------------------------------------------------------\
        //--------------------------------------------------------\
        [HttpGet]
        public IActionResult ShowCalculatePayForm()
        {
            return View("~/Views/Home/CalculatePay.cshtml");
        }



        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> CalculateContractValues(int contractId)
        {
            var claim = await _context.claim
                .Where(c => c.ClaimID == contractId)
                .Select(c => new
                {
                    c.ClaimID,
                    c.hoursWorked,
                    c.hourlyPay,
                    ContractValue = c.hoursWorked * c.hourlyPay
                })
                .FirstOrDefaultAsync();

            if (claim == null)
            {
                return NotFound();
            }

            return View("~/Views/Home/CalculatePay.cshtml", claim);
        }


        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                ViewBag.LecturerID = lecturer.LecturerID; // Pass the LecturerID to the view
                return View("~/Views/Home/AddUser.cshtml");
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
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        public IActionResult ClaimTrack()
        {
            return View("~/Views/Home/ClaimTrack.cshtml");
        }
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

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
                    ViewBag.ClaimID = claim.ClaimID; // Pass the ClaimID to the view

                    // Validate the claim after creation
                    var validationErrors = new List<string>();

                    if (claim.hoursWorked < 50)
                    {
                        validationErrors.Add("Lecturer must have worked a minimum of 50 hours.");
                    }

                    if (claim.hoursWorked > 500)
                    {
                        validationErrors.Add("Lecturer cannot have worked more than 500 hours.");
                    }

                    if (claim.hourlyPay > 30)
                    {
                        validationErrors.Add("Hourly pay cannot be more than thirty rand.");
                    }

                    if (validationErrors.Any())
                    {
                        ViewBag.ValidationResult = "Failed";
                        ViewBag.ValidationErrors = validationErrors;
                    }
                    else
                    {
                        ViewBag.ValidationResult = "Success";
                    }

                    return View("~/Views/Home/ClaimTrack.cshtml", claim);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating claim");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the claim.");
                }
            }
            return View("~/Views/Home/ClaimTrack.cshtml", claim);
        }


        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult AcademicManager()
        {
            return View("~/Views/Home/AcademicManagerView.cshtml");
        }
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcademicManager(AcademicManager academicManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicManager);
                await _context.SaveChangesAsync();
                ViewBag.AcademicManagerID = academicManager.AcademicManagerID; // Pass the LecturerID to the view
                return View("~/Views/Home/AcademicManagerView.cshtml");
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
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult ProgCoOrdinator()
        {
            return View("~/Views/Home/ProgCoOrdinator.cshtml");
        }
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProgCoOrdinator(ProgCoOrdinator progCoOrdinator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progCoOrdinator);
                await _context.SaveChangesAsync();
                ViewBag.ProgCoOrdinatorID = progCoOrdinator.ProgCoOrdinatorID; // Pass the LecturerID to the view
                return View("~/Views/Home/ProgCoOrdinator.cshtml");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View("~/Views/Home/Index.cshtml", progCoOrdinator);
            }
        }
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult ClaimApproval()
        {
            return View("~/Views/Home/ClaimApprovalView.cshtml");
        }
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

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
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

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
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------




        [HttpGet]
        public IActionResult Upload()
        {
            return View("~/Views/Home/UploadFile.cshtml", new Document());
        }
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

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
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var documents = await _context.document.ToListAsync();
            return View("~/Views/Home/Index.cshtml", documents);
        }
        //----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

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
