using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Principal.Models;
using Principal.Repositories;

namespace Principal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramadorController : ControllerBase
    {
        private readonly IProgramadorRepository repo;
        public ProgramadorController(IProgramadorRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Programador>> GetProgramadores() {
            var progs = repo.GetProgramadores();
            return Ok(progs);
        }
        [HttpGet("{id}")]
        public ActionResult<Programador> GetProgramadorById(int id)
        {
            var prog = repo.GetProgramadorById(id);
            if (prog == null)
                return NotFound();
            return Ok(prog);
        }
    }
}
