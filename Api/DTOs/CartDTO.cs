namespace Api.DTOs
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }

    public class CUCartDTO
    {
        public List<CUCartItemDTO> CartItems { get; set; }
    }
}
