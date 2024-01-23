using Product.API.Category.Infrastructure.Entities;

namespace Product.API.Category.DTO.InternalAPI.Response
{
    public class CategoryResponse
    {
        public int ParentCatId { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
