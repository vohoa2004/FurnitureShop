using FurnitureShop.DAL.Entities;

namespace FurnitureShop.API.DTOs.RequestDTOs
{
    public class OrderRequestDTO
    {
        public Guid CustomerId { get; set; }
        public required List<OrderLineRequestDTO> Lines { get; set; }
        public int TotalPrice { get; set; }
        public required PaymentMethod PaymentMethod { get; set; }
        public required bool IsPaid { get; set; }
    }
}
