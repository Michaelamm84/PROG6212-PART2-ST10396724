﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
/*
 * Author: Michael AMM.
 * ST10396724
 * ProgPOEPart3
 * Group 1
 */

namespace PROG6212_PART2_ST10396724.Models
{
    public class ProgCoOrdinator
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgCoOrdinatorID { get; set; }

        public string ProgCoOrdinatorName { get; set; }

        public string progEmail { get; set; }


        private ICollection<ClaimApproval> claimApprovals { get; set; } = new List<ClaimApproval>();
    }
}
