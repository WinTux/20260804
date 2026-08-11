using System.ComponentModel.DataAnnotations;

namespace Principal.DTO
{
    public class ProgramadorCreateDTO
    {
        [MaxLength(50)]
        [MinLength(3)]
        public string nick { get; set; }
    }
}
