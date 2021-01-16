using AutoMapper;
using Domain.Models.DTO;
using Infrastructure.Persistence.Entities;

namespace Api.Configuration
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
