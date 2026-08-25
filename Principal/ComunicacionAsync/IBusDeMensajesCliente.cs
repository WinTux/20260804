using Principal.DTO;

namespace Principal.ComunicacionAsync
{
    public interface IBusDeMensajesCliente
    {
        void PublicarNuevoProgramador(ProgramadorPublisherDTO programadorPublisherDTO);
    }
}
