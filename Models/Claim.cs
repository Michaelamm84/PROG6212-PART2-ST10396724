using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
 * Author: Michael AMM.
 * ST10396724
 * ProgPOEPart2
 * 
 */


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

        public decimal ContractValue => hoursWorked * hourlyPay; // Calculated proper


        public string Notes { get; set; }

        

           [Required]
            public int LecturerID { get; set; }


            public Lecturer? lecturer { get; set; }

        private ICollection<ClaimApproval> claimApprovals { get; set; } = new List<ClaimApproval>();

       public ICollection<Document> documentsClaim { get; set; } = new List<Document>();


    }
}
