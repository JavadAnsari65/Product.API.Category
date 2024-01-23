using AutoMapper;
using Product.API.Category.DTO.InternalAPI.Request;
using Product.API.Category.DTO.InternalAPI.Response;
using Product.API.Category.Infrastructure.Entities;
using Product.API.Category.Infrastructure.Repository;
using Product.API.ProductCatalog.Extensions.SearchClasses;
using System.Linq.Expressions;

namespace Product.API.Category.Application
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly CRUDService _crudService;
        public CategoryRepository(IMapper mapper, CRUDService crudService)
        {
            _mapper = mapper;
            _crudService = crudService;
        }

        private DTO.ExternalAPI.Response.CategoryResponse GetErrorResponse(string errorMessage)
        {
            return new DTO.ExternalAPI.Response.CategoryResponse
            {
                Name = errorMessage,
                Description = "An error has occurred",
            };
        }
        
        public List<DTO.ExternalAPI.Response.CategoryResponse> GetAllCategory()
        {
            var catList = _crudService.GetAllCategoryOfDB();
            var catListResult = _mapper.Map<List<DTO.ExternalAPI.Response.CategoryResponse>>(catList);
            return catListResult;
        }

        public List<DTO.ExternalAPI.Response.CategoryResponse> SearchCategory(string fieldName, string fieldValue)
        {
            var query = _crudService.CreateQuery();
            var filterService = new EntityFilterService<CategoryEntity>(query);
            var parameter = Expression.Parameter(typeof(CategoryEntity), fieldName);
            var property = Expression.Property(parameter, fieldName);

            var convertedValue = Convert.ChangeType(fieldValue, property.Type);

            var constant = Expression.Constant(convertedValue);
            var equals = Expression.Equal(property, constant);
            var lambada = Expression.Lambda<Func<CategoryEntity, bool>>(equals, parameter);

            var catResult = _crudService.SearchCatInDB(filterService, lambada);

            var catMap = _mapper.Map<List<DTO.ExternalAPI.Response.CategoryResponse>>(catResult);
            return catMap;
        }

        public DTO.ExternalAPI.Response.CategoryResponse AddCategory(DTO.ExternalAPI.Request.CategoryRequest newCategory)
        {
            try
            {
                var internalCat = _mapper.Map<CategoryRequest>(newCategory);
                var category = _mapper.Map<CategoryEntity>(internalCat);
                var addCatResult = _crudService.AddCategoryInDB(category);

                var catResult = _mapper.Map<DTO.ExternalAPI.Response.CategoryResponse>(addCatResult);
                return catResult;
            }
            catch (Exception ex)
            {

                return GetErrorResponse(ex.Message);
            }
            
        }

        public DTO.ExternalAPI.Response.CategoryResponse UpdateCategory(int catId, DTO.ExternalAPI.Request.CategoryRequest category)
        {
            try
            {
                var internalCat = _mapper.Map<CategoryRequest>(category);

                var categoryEnty = _mapper.Map<CategoryEntity>(internalCat);
                var updateCatResult = _crudService.UpdateCategoryInDB(catId, categoryEnty);

                var catResult = _mapper.Map<DTO.ExternalAPI.Response.CategoryResponse>(updateCatResult);
                return catResult;
            }
            catch (Exception ex)
            {

                return GetErrorResponse(ex.Message);
            }
        }

        public string DeleteCategory(int catId)
        {
            var delCatResult = _crudService.DeleteCategoryOfDB(catId);
            return delCatResult;
        }
    }
}
