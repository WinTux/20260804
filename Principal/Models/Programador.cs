using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Principal.Models
{
    [Table("programador")]
    public class Programador
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string nick { get; set; }
        [MaxLength(50)]
        public string? especialidad { get; set; }

    }
}
