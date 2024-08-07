using AutoMapper;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Softwares;

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

    }
}
