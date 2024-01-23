using AutoMapper;
using Product.API.Category.DTO.InternalAPI.Request;
using Product.API.Category.DTO.InternalAPI.Response;
using Product.API.Category.Infrastructure.Entities;

namespace Product.API.Category.Infrastructure.Configuration
{
    public class CustomMap:Profile
    {
        public CustomMap()
        {
            CreateMap<CategoryEntity, CategoryResponse>().ReverseMap();
            CreateMap<CategoryEntity, CategoryRequest>().ReverseMap();

            CreateMap<CategoryEntity, DTO.ExternalAPI.Response.CategoryResponse>().ReverseMap();
            CreateMap<CategoryRequest, DTO.ExternalAPI.Request.CategoryRequest>().ReverseMap();
            CreateMap<CategoryResponse, DTO.ExternalAPI.Response.CategoryResponse>().ReverseMap();

        }
    }
}
