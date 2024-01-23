using Product.API.Category.DTO.InternalAPI.Response;
using Product.API.Category.Infrastructure.Configuration;
using Product.API.Category.Infrastructure.Entities;
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

        public List<CategoryEntity> GetAllCategoryOfDB()
        {
            try
            {
                var catList = _dbContext.Categories
                    .OrderBy(p => p.CatId)
                    .ThenBy(p => p.ParentCatId)
                    .ToList();
                return catList;
   
            }
            catch (Exception ex)
            {
                var lst = new List<CategoryEntity>();
                lst.Add(GetErrorResponse(ex.Message));
                return lst;
            }
        }

        public IQueryable<CategoryEntity> CreateQuery()
        {
            var query = _dbContext.Categories.AsQueryable();
            return query;
        }

        public List<CategoryEntity> SearchCatInDB(EntityFilterService<CategoryEntity> filterService, Expression<Func<CategoryEntity, bool>> lambada)
        {
            var query = filterService.ApplyFilter(lambada);
            var catResult = query.ToList();
            return catResult;
        }

        public CategoryEntity AddCategoryInDB(CategoryEntity newCategory)
        {
            try
            {
                //بررسی اینکه شناسه والد واردشده در دیتابیس وجود داشته باشد
                var isParentIdValid = _dbContext.Categories.FirstOrDefault(c => c.CatId == newCategory.ParentCatId);

                if (isParentIdValid != null) 
                {
                    _dbContext.Categories.Add(newCategory);
                    _dbContext.SaveChanges();
                    return (newCategory);
                }
                else
                {
                    throw new Exception("ParentCatId is Not Found in Categories");
                }
            }
            catch (Exception ex)
            {
                return (GetErrorResponse(ex.Message));
            }
        }

        public CategoryEntity UpdateCategoryInDB(int catId, CategoryEntity category)
        {
            try
            {
                var existingCat = _dbContext.Categories.FirstOrDefault(c => c.CatId == catId);

                if (existingCat != null)
                {
                    existingCat.ParentCatId = category.ParentCatId;
                    existingCat.Name = category.Name;
                    existingCat.Description = category.Description;

                    _dbContext.SaveChanges();
                    return (existingCat);
                }
                else
                {
                    return GetErrorResponse($"Category with CatId={catId} is not found");
                }

            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        public string DeleteCategoryOfDB(int catId)
        {
            try
            {
                var existingCat = _dbContext.Categories.FirstOrDefault(c => c.CatId == catId);
                if (existingCat != null)
                {
                    _dbContext.Categories.Remove(existingCat);
                    _dbContext.SaveChanges();
                    return "DeleteSuccess";
                }
                else
                {
                    return ($"Category Not Found");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
