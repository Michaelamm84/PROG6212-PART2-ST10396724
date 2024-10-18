using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6212_PART2_ST10396724.Models
{



    [Table("test")]
    public class test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
