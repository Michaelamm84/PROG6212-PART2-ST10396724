using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
 * Author: Michael AMM.
 * ST10396724
 * ProgPOEPart3
 * Group 1
 */

namespace PROG6212_PART2_ST10396724.Models
{
    public class ClaimApproval
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimApprovalID { get; set; }

        
        public string newApprovalStatus { get; set; } = "Pending"; // Initialized with a default value


        public int ProgCoOrdinatorID { get; set; }

      
        public int ClaimID { get; set; }

       
        public int AcademicManagerID { get; set; }

        
        public int LecturerID { get; set; }




        public Lecturer? lecturer { get; set; }
        public AcademicManager? AcademicManagers { get; set; }
        public ProgCoOrdinator? progs { get; set; }
        public Claim? claim { get; set; }
    }
}