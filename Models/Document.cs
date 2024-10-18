using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROG6212_PART2_ST10396724.Models
{
    public class Document
    {


        
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string FileName { get; set; }

            public byte[] FileBinary { get; set; } // Store the file binary

            public int ClaimID { get; set; }

            public int LecturerID { get; set; }

            public Lecturer? lecturer { get; set; }
            public Claim? claim { get; set; }

        
    }
}
