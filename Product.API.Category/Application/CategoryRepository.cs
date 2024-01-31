using AutoMapper;
using Microsoft.AspNetCore.SignalR.Protocol;
using Product.API.Category.DTO.InternalAPI.Request;
using Product.API.Category.DTO.InternalAPI.Response;
using Product.API.Category.Infrastructure.Entities;
using Product.API.Category.Infrastructure.Repository;
using Product.API.ProductCatalog.Extensions.ExtraClasses;
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
        
        public ApiResponse<List<CategoryResponse>> GetAllCategory()
        {
            try
            {
                var catList = _crudService.GetAllCategoryOfDB();
                var catListResult = _mapper.Map<List<CategoryResponse>>(catList.Data);

                if (catList.Result)
                {
                    return new ApiResponse<List<CategoryResponse>>
                    {
                        Result = true,
                        Data = catListResult
                    };
                }
                else
                {
                    return new ApiResponse<List<CategoryResponse>>
                    {
                        Result = false,
                        ErrorMessage = catList.ErrorMessage,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryResponse>>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public ApiResponse<List<CategoryResponse>> SearchCategory(string fieldName, string fieldValue)
        {
            try
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

                var catMap = _mapper.Map<List<CategoryResponse>>(catResult.Data);

                if (catResult.Result)
                {
                    return new ApiResponse<List<CategoryResponse>>
                    {
                        Result = catResult.Result,
                        Data = catMap
                    };
                }
                else
                {
                    return new ApiResponse<List<CategoryResponse>>
                    {
                        Result = false,
                        ErrorMessage = catResult.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryResponse>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<CategoryResponse> AddCategory(CategoryRequest newCategory)
        {
            try
            {
                var category = _mapper.Map<CategoryEntity>(newCategory);
                var addCatResult = _crudService.AddCategoryInDB(category);

                if (addCatResult.Result)
                {
                    var catResult = _mapper.Map<CategoryResponse>(addCatResult.Data);

                    return new ApiResponse<CategoryResponse>
                    {
                        Result = addCatResult.Result,
                        Data = catResult
                    };
                }
                else
                {
                    return new ApiResponse<CategoryResponse>
                    {
                        Result = addCatResult.Result,
                        ErrorMessage = addCatResult.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse<CategoryResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
            
        }

        public ApiResponse<CategoryResponse> UpdateCategory(int catId, CategoryRequest category)
        {
            try
            {
                var internalCat = _mapper.Map<CategoryRequest>(category);

                var categoryEnty = _mapper.Map<CategoryEntity>(internalCat);
                var updateCatResult = _crudService.UpdateCategoryInDB(catId, categoryEnty);

                if(updateCatResult.Result)
                {
                    var catResult = _mapper.Map<CategoryResponse>(updateCatResult.Data);

                    return new ApiResponse<CategoryResponse>
                    {
                        Result = updateCatResult.Result,
                        Data = catResult
                    };
                }
                else
                {
                    return new ApiResponse<CategoryResponse>
                    {
                        Result = updateCatResult.Result,
                        ErrorMessage = updateCatResult.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse<CategoryResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<string> DeleteCategory(int catId)
        {
            try
            {
                var delCatResult = _crudService.DeleteCategoryOfDB(catId);

                if(delCatResult.Result) 
                {
                    return new ApiResponse<string>
                    {
                        Result = delCatResult.Result,
                        Data = delCatResult.Data
                    };
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        Result = delCatResult.Result,
                        ErrorMessage = delCatResult.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
