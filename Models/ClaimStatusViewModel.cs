
/*
 * Author: Michael AMM.
 * ST10396724
 * ProgPOEPart3
 * Group 1
 */
namespace PROG6212_PART2_ST10396724.Models
{
    public class ClaimStatusViewModel
    {
        public List<Claim> PendingClaims { get; set; }
        public List<ClaimApproval> PendingClaimApprovals { get; set; }
        public List<ClaimApproval> ApprovedClaims { get; set; }
        public List<ClaimApproval> DeniedClaims { get; set; }
    }
}
