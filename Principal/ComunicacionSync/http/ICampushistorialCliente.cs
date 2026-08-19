using Principal.DTO;

namespace Principal.ComunicacionSync.http
{
    public interface ICampushistorialCliente
    {
        Task ComunicarseConCampus(ProgramadorReadDTO prog);
    }
}
