namespace Product.API.Category.DTO.InternalAPI.Request
{
    public class CategoryRequest
    {
        public int CatId { get; set; }
        public int ParentCatId { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
