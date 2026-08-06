using Principal.Models;

namespace Principal.Repositories
{
    public class ImplProgramadorRepository : IProgramadorRepository
    {
        private readonly PrincipalDbContext context;
        public ImplProgramadorRepository(PrincipalDbContext context)
        {
            this.context = context;
        }
        public Programador GetProgramadorById(int id)
        {
            return context.Programadores.FirstOrDefault(p => p.id == id);
        }

        public IEnumerable<Programador> GetProgramadores()
        {
            return context.Programadores.ToList();
        }
    }
}
