namespace RentX.RentXDTOs;

public class GetPropertyDTO
{
    public int Id { get; set; }
    public required string Address { get; set; }
    public required int RentLow { get; set; }
    public required int RentHigh { get; set; }
    public required string City { get; set; }
    public required int PropertyTypeId { get; set; }
    public required string? PropertyName { get; set; }

}
