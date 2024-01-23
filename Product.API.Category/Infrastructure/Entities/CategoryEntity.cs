using System.ComponentModel.DataAnnotations;

namespace Product.API.Category.Infrastructure.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int CatId { get; set; }
        public int ParentCatId { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
