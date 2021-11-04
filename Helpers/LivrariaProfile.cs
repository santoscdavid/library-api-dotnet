using AutoMapper;
using LivrariaAPI.Data.Dtos;
using LivrariaAPI.Models;
namespace LivrariaAPI.Helpers
{
    public class LivrariaProfile : Profile
    {
        public LivrariaProfile()
        {
            CreateMap<Livro, LivroDto>().ReverseMap();
            CreateMap<Livro, LivroResgisterDto>().ReverseMap();

            CreateMap<Editora, EditoraDto>().ReverseMap();
            CreateMap<Editora, EditoraRegisterDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDtoRegister>().ReverseMap();

            CreateMap<Aluguel, AluguelDto>().ReverseMap();
            CreateMap<Aluguel, AluguelDtoRegister>().ReverseMap();
        }
    }
}