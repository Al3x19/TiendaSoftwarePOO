using AutoMapper;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Lists;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.DTOS.Users;

namespace TiendaSoftware.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForPublishers();

        }

        private void MapsForPublishers()
        {
            CreateMap<PublisherEntity, PublisherDto>();
            CreateMap<PublisherCreateDto, PublisherEntity>();
            CreateMap<PublisherEditDto, PublisherEntity>();
        }

        private void MapsForSoftwares()
        {
            CreateMap<SoftwareEntity, SoftwareDto>().ForMember(destino => destino.Tags, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.tags.Name).ToList()));
            CreateMap<SoftwareCreateDto, SoftwareEntity>();
            CreateMap<SoftwareEditDto, SoftwareEntity>();
        }

        private void MapsForUsers()
        {
            CreateMap<UserEntity, UserDto>().ForMember(destino => destino.Compras, opt => opt.MapFrom(src => src.Compras.Select(pt => pt.Software.Name).ToList()));
            CreateMap<UserCreateDto, UserEntity>();
            CreateMap<UserEditDto, UserEntity>();
        }

        private void MapsForLists()
        {
            CreateMap<ListEntity, ListDto>().ForMember(destino => destino.Softwares, opt => opt.MapFrom(src => src.Softwares.Select(pt => pt.Software.Name).ToList()));
            CreateMap<ListCreateDto, ListEntity>();
            CreateMap<ListEditDto, ListEntity>();
        }


    }
}
