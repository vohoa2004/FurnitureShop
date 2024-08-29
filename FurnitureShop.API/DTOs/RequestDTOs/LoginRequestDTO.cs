namespace FurnitureShop.API.DTOs.RequestDTOs
{
    public class LoginRequestDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
