namespace FurnitureShop.API.DTOs.RequestDTOs
{
    public class OrderLineRequestDTO
    {
        public int ProductId { get; set; }
        public int Quantity {  get; set; }
        public double LinePrice { get; set; }

    }
}
