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

        public void AddProgramador(Programador prog)
        {
            if(prog == null)
                throw new ArgumentNullException(nameof(prog));
            context.Programadores.Add(prog);
        }

        public void DeleteProgramador(Programador prog)
        {
            if (prog == null)
                throw new ArgumentNullException(nameof(prog));
            context.Programadores.Remove(prog);
        }

        public Programador GetProgramadorById(int id)
        {
            return context.Programadores.FirstOrDefault(p => p.id == id);
        }

        public IEnumerable<Programador> GetProgramadores()
        {
            return context.Programadores.ToList();
        }

        public bool Guardar()
        {
            return context.SaveChanges() >= 0;
        }

        public void UpdateProgramador(Programador prog)
        {
            //
        }
    }
}
