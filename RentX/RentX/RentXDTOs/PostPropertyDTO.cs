using RentX.Models;

namespace RentX.RentXDTOs
{
    public class PostPropertyDTO
    {
        
        public required string Address { get; set; }
        public required int RentLow { get; set; }
        public required int RentHigh { get; set; }
        public required string City { get; set; }
        public required int PropertyTypeId { get; set; }
        public required int UserId { get; set; }

    }
}
