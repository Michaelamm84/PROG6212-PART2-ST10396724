using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6212_PART2_ST10396724.Models
{
    public class Claim
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ClaimID { get; set; }

            public int hoursWorked { get; set; }

            public int hourlyPay { get; set; }

             public string ApprovalStatus { get; set; } = "Pending"; // Initialized with a default value

         
        

        //public string approvalStatus { get; set; }

            public string Notes { get; set; }

        // Foreign key to Lecturer

           [Required]
            public int LecturerID { get; set; }

        // Navigation property

            public Lecturer? lecturer { get; set; }

        private ICollection<ClaimApproval> claimApprovals { get; set; } = new List<ClaimApproval>();


    }
}
