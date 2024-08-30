namespace FurnitureShop.API.DTOs.RequestDTOs
{
    public class SearchFurnitureRequestDTO
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? Name { get; set; }
        public string? CategoryIds { get; set; } // Change from List<int> to string
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 8;

        public List<int>? GetCategoryIdsList()
        {
            if (!string.IsNullOrEmpty(CategoryIds))
            {
                return CategoryIds.Split(',')
                                  .Select(int.Parse)
                                  .ToList();
            }
            return null;
        }
    }

}
