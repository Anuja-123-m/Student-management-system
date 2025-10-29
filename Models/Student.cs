using System.ComponentModel.DataAnnotations;

namespace StudentRecordManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string RollNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Maths { get; set; }
        [Range(1, 100)]
        public int Physics { get; set; }
        [Range(1, 100)]
        public int Chemistry { get; set; }
        [Range(1, 100)]
        public int English { get; set; }
        [Range(1, 100)]
        public int Programming { get; set; }
    }
}
