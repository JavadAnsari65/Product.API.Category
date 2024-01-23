using Microsoft.AspNetCore.Mvc;
using Product.API.Category.Application;

namespace Product.API.Category.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;   
        }

        [HttpGet]
        public ActionResult<List<DTO.ExternalAPI.Response.CategoryResponse>> GetAllCategory()
        {
            var catList = _categoryRepo.GetAllCategory();
            return catList;
        }

        [HttpGet]
        public ActionResult<DTO.ExternalAPI.Response.CategoryResponse> SearchCategory(string fieldName, string fieldValue)
        {
            var productResult = _categoryRepo.SearchCategory(fieldName, fieldValue);
            return Ok(productResult);
        }

        [HttpPost]
        public ActionResult<DTO.ExternalAPI.Response.CategoryResponse> AddCategory(DTO.ExternalAPI.Request.CategoryRequest newCategory)
        {
            var addCatResult = _categoryRepo.AddCategory(newCategory);
            return (addCatResult);
        }

        [HttpPut]
        public ActionResult<DTO.ExternalAPI.Response.CategoryResponse> UpdateCategory(int catId, DTO.ExternalAPI.Request.CategoryRequest category)
        {
            var updateCatResult = _categoryRepo.UpdateCategory(catId, category);
            return (updateCatResult);
        }

        [HttpDelete]
        public ActionResult<string> DeleteCategory(int catId) 
        {
            var delCatResult = _categoryRepo.DeleteCategory(catId);
            return Ok(delCatResult);
        }
    }
}
