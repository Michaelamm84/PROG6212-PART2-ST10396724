using System.ComponentModel.DataAnnotations.Schema;
/*
 * Author: Michael AMM.
 * ST10396724
 * ProgPOEPart3
 * Group 1
 */

namespace PROG6212_PART2_ST10396724.Models
{



    [Table("test")]
    public class test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
