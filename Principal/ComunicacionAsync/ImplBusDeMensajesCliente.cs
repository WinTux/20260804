using Principal.DTO;
using RabbitMQ.Client;

namespace Principal.ComunicacionAsync
{
    public class ImplBusDeMensajesCliente : IBusDeMensajesCliente
    {
        private readonly IConfiguration configuration;
        private readonly IConnection connection;
        private readonly IModel canal;
        public ImplBusDeMensajesCliente(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = configuration["Host_RabbitMQ"],
                Port = int.Parse(configuration["Puerto_RabbitMQ"])
            };
            try
            {
                connection = factory.CreateConnection();
                canal = connection.CreateModel();
                canal.ExchangeDeclare(
                    exchange: "mi_exchange",
                    type: ExchangeType.Fanout
                );
            } catch (Exception ex)
            {
                Console.WriteLine("Error al conectarse a RabbitMQ: " + ex.Message); 
            }
        }
        public void PublicarNuevoProgramador(ProgramadorPublisherDTO programadorPublisherDTO)
        {
            string mensaje = System.Text.Json.JsonSerializer.Serialize(programadorPublisherDTO);
            if (connection.IsOpen)
                Enviar(mensaje);
            else
                Console.WriteLine("No se puede enviar el mensaje, la conexión a RabbitMQ no está abierta.");
        }
        private void Enviar(string msj) { 
            var cuerpo = System.Text.Encoding.UTF8.GetBytes(msj);
            canal.BasicPublish(
                exchange: "mi_exchange",
                routingKey: "",
                basicProperties: null,
                body: cuerpo
            );
            Console.WriteLine("Mensaje enviado a RabbitMQ: " + msj);
        }
    }
}
