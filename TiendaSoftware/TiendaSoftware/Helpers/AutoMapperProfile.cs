using AutoMapper;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Categories;
using TiendaSoftware.API.DTOS.Posts;

namespace TiendaSoftware.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForCategories();
            MapsForPosts();
        }

        private void MapsForCategories()
        {
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryCreateDto, CategoryEntity>();
            CreateMap<CategoryEditDto, CategoryEntity>();
        }

        private void MapsForPosts() 
        {
            CreateMap<PostEntity, PostDto>().ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.Tag.Name).ToList()));
            CreateMap<PostCreaDto, PostEntity>();
            CreateMap<PostEditDto, PostEntity>();
        }
    }
}
