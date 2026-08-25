using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Principal.ComunicacionAsync;
using Principal.ComunicacionSync.http;
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
        private readonly ICampushistorialCliente campushistorialCliente;
        private readonly IBusDeMensajesCliente busDeMensajesCliente;
        public ProgramadorController(IProgramadorRepository repo, IMapper mapper, ICampushistorialCliente campushistorialCliente, IBusDeMensajesCliente busDeMensajesCliente)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.campushistorialCliente = campushistorialCliente;
            this.busDeMensajesCliente = busDeMensajesCliente;
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
        public async Task<ActionResult<ProgramadorReadDTO>> CreateProgramador([FromBody] ProgramadorCreateDTO dto)
        {
            Programador prog = mapper.Map<Programador>(dto);
            repo.AddProgramador(prog);
            if (!repo.Guardar())
                return BadRequest();
            try
            {
                var prdto = mapper.Map<ProgramadorReadDTO>(prog);
                await campushistorialCliente.ComunicarseConCampus(prdto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar el mensaje a Campushistorial: {ex.Message}");
            }
            try
            {
                var prdto = mapper.Map<ProgramadorReadDTO>(prog);
                var proPublish = mapper.Map<ProgramadorPublisherDTO>(prdto);
                proPublish.tipoEvento = "Programador_Creado";
                busDeMensajesCliente.PublicarNuevoProgramador(proPublish);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar el mensaje a RabbitMQ: {ex.Message}");
            }
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

        [HttpPatch("{id}")]
        public ActionResult UpdateParcialProgramador(int id, JsonPatchDocument<ProgramadorUpdateDTO> progPatch) {
            Programador prog = repo.GetProgramadorById(id);
            if (prog == null)
                return NotFound();
            ProgramadorUpdateDTO progParaPatch = mapper.Map<ProgramadorUpdateDTO>(prog);
            progPatch.ApplyTo(progParaPatch, ModelState);
            if (!TryValidateModel(progParaPatch))
                return ValidationProblem(ModelState);
            mapper.Map(progParaPatch, prog);
            repo.UpdateProgramador(prog);
            repo.Guardar();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult EliminarProgramador(int id) {
            Programador prog = repo.GetProgramadorById(id);
            if (prog == null)
                return NotFound();
            repo.DeleteProgramador(prog);
            repo.Guardar();
            return NoContent();
        }
    }
}
