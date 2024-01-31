using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Category.Application;
using Product.API.Category.DTO.InternalAPI.Request;
using System.Collections.Generic;

namespace Product.API.Category.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
        {

            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<DTO.ExternalAPI.Response.CategoryResponse>> GetAllCategory()
        {
            try
            {
                var catList = _categoryRepo.GetAllCategory();
                var mapExternalcatResult = _mapper.Map<List<DTO.ExternalAPI.Response.CategoryResponse>>(catList.Data);

                if (catList.Result)
                {
                    return mapExternalcatResult;
                }
                else
                {
                    return BadRequest(catList.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<DTO.ExternalAPI.Response.CategoryResponse> SearchCategory(string fieldName, string fieldValue)
        {
            try
            {
                var productResult = _categoryRepo.SearchCategory(fieldName, fieldValue);
                var mapExternalProductResult = _mapper.Map<List<DTO.ExternalAPI.Response.CategoryResponse>>(productResult.Data);

                if (productResult.Result)
                {
                    return Ok(mapExternalProductResult);
                }
                else
                {
                    return BadRequest(productResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<DTO.ExternalAPI.Response.CategoryResponse> AddCategory(DTO.ExternalAPI.Request.CategoryRequest newCategory)
        {
            try
            {
                var mapInternalNewCat = _mapper.Map<CategoryRequest>(newCategory);
                var addCatResult = _categoryRepo.AddCategory(mapInternalNewCat);

                if (addCatResult.Result)
                {
                    var mapExternalCatResult = _mapper.Map<DTO.ExternalAPI.Response.CategoryResponse>(addCatResult.Data);
                    return Ok(addCatResult.Data);
                }
                else
                {
                    return BadRequest(addCatResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<DTO.ExternalAPI.Response.CategoryResponse> UpdateCategory(int catId, DTO.ExternalAPI.Request.CategoryRequest category)
        {
            try
            {
                var mapInternalCategory = _mapper.Map<CategoryRequest>(category);
                var updateCatResult = _categoryRepo.UpdateCategory(catId, mapInternalCategory);
                
                if (updateCatResult.Result)
                {
                    var mapExternalUpdateCat = _mapper.Map<DTO.ExternalAPI.Response.CategoryResponse>(updateCatResult.Data);
                    return Ok(mapExternalUpdateCat);
                }
                else
                {
                    return BadRequest(updateCatResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<string> DeleteCategory(int catId) 
        {
            try
            {
                var delCatResult = _categoryRepo.DeleteCategory(catId);
                if (delCatResult.Result)
                {
                    return Ok(delCatResult.Data);
                }
                else
                {
                    return BadRequest(delCatResult.ErrorMessage);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
