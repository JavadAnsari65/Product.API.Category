namespace Product.API.Category.DTO.ExternalAPI.Request
{
    public class CategoryRequest
    {
        public int ParentCatId { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
