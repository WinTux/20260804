using Principal.Models;

namespace Principal.Repositories
{
    public interface IProgramadorRepository
    {
        IEnumerable<Programador> GetProgramadores();
        Programador GetProgramadorById(int id);
        void AddProgramador(Programador prog);
        void UpdateProgramador(Programador prog);
        void DeleteProgramador(Programador prog);
        public bool Guardar();
    }
}
