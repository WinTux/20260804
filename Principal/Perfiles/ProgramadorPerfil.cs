using AutoMapper;
using Principal.DTO;
using Principal.Models;

namespace Principal.Perfiles
{
    public class ProgramadorPerfil : Profile
    {
        public ProgramadorPerfil()
        {
            CreateMap<Programador, ProgramadorReadDTO>(); // ---->
        }
    }
}
