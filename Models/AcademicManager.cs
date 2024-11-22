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
    public class AcademicManager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int AcademicManagerID { get; set; }

        public string AcademicManagerName { get; set; }

        public string acaEmail { get; set; }

        private ICollection<ClaimApproval> claimApprovals { get; set; } = new List<ClaimApproval>();
    }
}
