using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Principal.DTO;
using Principal.Models;
using Principal.Repositories;

namespace Principal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramadorController : ControllerBase
    {
        private readonly IProgramadorRepository repo;
        private readonly IMapper mapper;
        public ProgramadorController(IProgramadorRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProgramadorReadDTO>> GetProgramadores() {
            var progs = repo.GetProgramadores();
            return Ok(mapper.Map<IEnumerable<ProgramadorReadDTO>>(progs));
        }
        [HttpGet("{id}", Name = "GetProgramadorById")]
        public ActionResult<ProgramadorReadDTO> GetProgramadorById(int id)
        {
            Programador prog = repo.GetProgramadorById(id);
            if (prog == null)
                return NotFound();
            return Ok(mapper.Map<ProgramadorReadDTO>(prog));
        }
        [HttpPost]
        public ActionResult<ProgramadorReadDTO> CreateProgramador([FromBody] ProgramadorCreateDTO dto)
        {
            Programador prog = mapper.Map<Programador>(dto);
            repo.AddProgramador(prog);
            if (!repo.Guardar())
                return BadRequest();
            return CreatedAtRoute(nameof(GetProgramadorById), new { id = prog.id }, mapper.Map<ProgramadorReadDTO>(prog));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProgramador(int id, [FromBody] ProgramadorUpdateDTO dto) {
            Programador prog = repo.GetProgramadorById(id);
            if (prog == null)
                return NotFound();
            mapper.Map(dto, prog);
            repo.UpdateProgramador(prog);
            if (!repo.Guardar())
                return BadRequest();
            return Ok(mapper.Map<ProgramadorReadDTO>(prog));
        }
    }
}
