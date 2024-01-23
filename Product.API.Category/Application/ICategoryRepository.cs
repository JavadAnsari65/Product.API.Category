using Microsoft.AspNetCore.Mvc;
using Product.API.Category.DTO.InternalAPI.Request;
using Product.API.Category.DTO.InternalAPI.Response;

namespace Product.API.Category.Application
{
    public interface ICategoryRepository
    {
        public List<DTO.ExternalAPI.Response.CategoryResponse> GetAllCategory();
        public List<DTO.ExternalAPI.Response.CategoryResponse> SearchCategory(string fieldName, string fieldValue);
        public DTO.ExternalAPI.Response.CategoryResponse AddCategory(DTO.ExternalAPI.Request.CategoryRequest newCategory);
        public DTO.ExternalAPI.Response.CategoryResponse UpdateCategory(int catId, DTO.ExternalAPI.Request.CategoryRequest category);
        public string DeleteCategory(int catId);

    }
}
