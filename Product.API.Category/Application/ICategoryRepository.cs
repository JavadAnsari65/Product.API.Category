using Microsoft.AspNetCore.Mvc;
using Product.API.Category.DTO.InternalAPI.Request;
using Product.API.Category.DTO.InternalAPI.Response;
using Product.API.ProductCatalog.Extensions.ExtraClasses;

namespace Product.API.Category.Application
{
    public interface ICategoryRepository
    {

        //public ApiResponse<List<DTO.ExternalAPI.Response.CategoryResponse>> GetAllCategory();
        public ApiResponse<List<CategoryResponse>> GetAllCategory();


        //public ApiResponse<List<DTO.ExternalAPI.Response.CategoryResponse>> SearchCategory(string fieldName, string fieldValue);
        public ApiResponse<List<CategoryResponse>> SearchCategory(string fieldName, string fieldValue);

        //public ApiResponse<DTO.ExternalAPI.Response.CategoryResponse> AddCategory(DTO.ExternalAPI.Request.CategoryRequest newCategory);
        public ApiResponse<CategoryResponse> AddCategory(CategoryRequest newCategory);

        //public ApiResponse<DTO.ExternalAPI.Response.CategoryResponse> UpdateCategory(int catId, DTO.ExternalAPI.Request.CategoryRequest category);
        public ApiResponse<CategoryResponse> UpdateCategory(int catId, CategoryRequest category);

        public ApiResponse<string> DeleteCategory(int catId);

    }
}
