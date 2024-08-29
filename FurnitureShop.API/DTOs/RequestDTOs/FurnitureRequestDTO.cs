using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.API.DTOs.RequestDTOs
{
    public class FurnitureRequestDTO
    {
        [Required(ErrorMessage = "Furniture name is a nrequired field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for Description is 500 characters.")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price is required and it can't be lower than 0")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "quantity is required and it can't be lower than 0")]
        public int AvailableQuantity { get; set; }
        public string? ImageLink { get; set; }

        [Required]
        public bool IsShown { get; set; }

        [Required]
        public int CategoryId { get; set; } // Khóa ngoại cho Category
    }
}
