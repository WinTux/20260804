using Principal.Models;

namespace Principal.Repositories
{
    public class ImplProgramadorRepository : IProgramadorRepository
    {
        public Programador GetProgramadorById(int id)
        {
            return new Programador { id = id, nick = "DevMaster", especialidad = "Backend" };
        }

        public IEnumerable<Programador> GetProgramadores()
        {
            var progs = new List<Programador> {
                new Programador{ id = 1, nick = "DevMaster", especialidad = "Backend" },
                new Programador{ id = 2, nick = "CodeNinja", especialidad = "Frontend" }
            };
            return progs;
        }
    }
}
