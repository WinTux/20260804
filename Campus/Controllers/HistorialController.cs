using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post() {
            Console.WriteLine("Post request received");
            return Ok("Respuesta exitosa desde el controlador Historial");
        }
    }
}
