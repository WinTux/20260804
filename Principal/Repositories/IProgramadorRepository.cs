using Principal.Models;

namespace Principal.Repositories
{
    public interface IProgramadorRepository
    {
        IEnumerable<Programador> GetProgramadores();
        Programador GetProgramadorById(int id);
    }
}
