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
            CreateMap<ProgramadorCreateDTO, Programador>();
            CreateMap<ProgramadorUpdateDTO, Programador>();
            CreateMap<Programador, ProgramadorUpdateDTO>();
            CreateMap<ProgramadorReadDTO, ProgramadorPublisherDTO>();
        }
    }
}
