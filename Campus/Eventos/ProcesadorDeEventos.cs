using Campus.DTO;
using System.Text.Json;

namespace Campus.Eventos
{
    public class ProcesadorDeEventos : IProcesadorDeEventos
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        public ProcesadorDeEventos(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public void ProcesarEvento(string mensaje)
        {
            var tipo = DeterminarEventoOcurrido(mensaje);
            switch (tipo) { 
                case TipoDeEvento.programador_creado:
                    agregarProgramador(mensaje);
                    break;
                case TipoDeEvento.programador_eliminado:
                    //eliminarProgramador(mensaje);
                    break;
            }
        }
        private TipoDeEvento DeterminarEventoOcurrido(string mensaje) { 
            EventoDTO  tipo = JsonSerializer.Deserialize<EventoDTO>(mensaje);
            switch (tipo.evento) { 
                case "programador_creado":
                    return TipoDeEvento.programador_creado;
                case "programador_eliminado":
                    return TipoDeEvento.programador_eliminado;
                default:
                    return TipoDeEvento.accion_desconocida;
            }
        }
        private void agregarProgramador(string mensajeProgramadorPublisherDTO)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IPerfilRepository>();
                var programadorDTO = JsonSerializer.Deserialize<ProgramadorPublisherDTO>(mensajeProgramadorPublisherDTO);
                repo.AgregarProgramador(programadorDTO);
            }
        }
        enum TipoDeEvento { 
            programador_creado,
            programador_eliminado,
            accion_desconocida
        }
    }
}
