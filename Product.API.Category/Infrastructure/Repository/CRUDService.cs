using Product.API.Category.DTO.InternalAPI.Response;
using Product.API.Category.Infrastructure.Configuration;
using Product.API.Category.Infrastructure.Entities;
using Product.API.ProductCatalog.Extensions.ExtraClasses;
using Product.API.ProductCatalog.Extensions.SearchClasses;
using System.Linq.Expressions;

namespace Product.API.Category.Infrastructure.Repository
{
    public class CRUDService
    {
        private readonly CategoryDbContext _dbContext;
        public CRUDService(CategoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private CategoryEntity GetErrorResponse(string errorMessage)
        {
            return new CategoryEntity
            {
                Name = errorMessage,
                Description = "An error has occurred",
            };
        }

        public ApiResponse<List<CategoryEntity>> GetAllCategoryOfDB()
        {
            try
            {
                var catList = _dbContext.Categories
                    .OrderBy(p => p.CatId)
                    .ThenBy(p => p.ParentCatId)
                    .ToList();

                return new ApiResponse<List<CategoryEntity>>
                {
                    Result = true,
                    Data = catList
                };
   
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryEntity>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public IQueryable<CategoryEntity> CreateQuery()
        {
            var query = _dbContext.Categories.AsQueryable();
            return query;
        }

        public ApiResponse<List<CategoryEntity>> SearchCatInDB(EntityFilterService<CategoryEntity> filterService, Expression<Func<CategoryEntity, bool>> lambada)
        {
            try
            {
                var query = filterService.ApplyFilter(lambada);
                var catResult = query.ToList();

                return new ApiResponse<List<CategoryEntity>>
                {
                    Result = true,
                    Data = catResult
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryEntity>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<CategoryEntity> AddCategoryInDB(CategoryEntity newCategory)
        {
            try
            {
                //بررسی اینکه شناسه والد واردشده در دیتابیس وجود داشته باشد
                var isParentIdValid = _dbContext.Categories.FirstOrDefault(c => c.CatId == newCategory.ParentCatId);

                if (isParentIdValid != null || newCategory.ParentCatId==0) 
                {
                    _dbContext.Categories.Add(newCategory);
                    _dbContext.SaveChanges();

                    return new ApiResponse<CategoryEntity>
                    {
                        Result = true,
                        Data = newCategory
                    };
                }
                else
                {
                    return new ApiResponse<CategoryEntity>
                    {
                        Result = false,
                        ErrorMessage = "ParentCatId is Not Found in Categories"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<CategoryEntity> UpdateCategoryInDB(int catId, CategoryEntity category)
        {
            try
            {
                var existingCat = _dbContext.Categories.FirstOrDefault(c => c.CatId == catId);
                var isParentIdCat = _dbContext.Categories.FirstOrDefault(p=>p.ParentCatId == category.ParentCatId);

                if (existingCat != null)
                {
                    if(isParentIdCat!=null)
                    {
                        existingCat.ParentCatId = category.ParentCatId;
                        existingCat.Name = category.Name;
                        existingCat.Description = category.Description;

                        _dbContext.SaveChanges();

                        return new ApiResponse<CategoryEntity>
                        {
                            Result = true,
                            Data = existingCat
                        };
                    }
                    else
                    {
                        return new ApiResponse<CategoryEntity>
                        {
                            Result = false,
                            ErrorMessage = $"Parent Category with CatId = {category.ParentCatId} is not found"
                        };
                    }
                }
                else
                {
                    return new ApiResponse<CategoryEntity>
                    {
                        Result = false,
                        ErrorMessage = $"Category with CatId = {catId} is not found"
                    };
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<string> DeleteCategoryOfDB(int catId)
        {
            try
            {
                var existingCat = _dbContext.Categories.FirstOrDefault(c => c.CatId == catId);
                if (existingCat != null)
                {
                    _dbContext.Categories.Remove(existingCat);
                    _dbContext.SaveChanges();

                    return new ApiResponse<string>
                    {
                        Result = true,
                        Data = "DeleteSuccess"
                    };
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        Result = false,
                        ErrorMessage = "CategoryNotFound"
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
