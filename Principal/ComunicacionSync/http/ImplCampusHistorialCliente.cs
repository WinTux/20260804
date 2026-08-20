using Principal.DTO;

namespace Principal.ComunicacionSync.http
{
    public class ImplCampusHistorialCliente : ICampushistorialCliente
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        public ImplCampusHistorialCliente(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }
        public async Task ComunicarseConCampus(ProgramadorReadDTO prog)
        {
            StringContent cuerpoHttp = new StringContent(System.Text.Json.JsonSerializer.Serialize(prog), System.Text.Encoding.UTF8, "application/json");
            var respuesta = await httpClient.PostAsync($"{configuration["CampusService"]}/api/c/historial", cuerpoHttp);
            if(respuesta.IsSuccessStatusCode)
                Console.WriteLine("Se ha comunicado con Campus correctamente (sincronicamente)");
            else
                Console.WriteLine("Error al comunicarse con Campus (sincronicamente): " + respuesta.StatusCode);
        }
    }
}
