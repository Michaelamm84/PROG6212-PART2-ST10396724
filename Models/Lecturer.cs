using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6212_PART2_ST10396724.Models
{


    [Table("lecturer")]
    public class Lecturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LecturerID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string LecturerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public ICollection<Claim> claims { get; set; } = new List<Claim>();

        private ICollection<ClaimApproval> claimApprovals { get; set; } = new List<ClaimApproval>();
    }
}
