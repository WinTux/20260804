using System.ComponentModel.DataAnnotations;

namespace Principal.DTO
{
    public class ProgramadorUpdateDTO
    {
        public string? especialidad { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string nick { get; set; }
    }
}
