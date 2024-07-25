using AutoMapper;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Publishers;

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

        
    }
}
