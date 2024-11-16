namespace RentX.Models;

public class User
{
    public required int Id { get; set; }
    public required string FirstName  { get; set; }
    public string? LastName { get; set; } = string.Empty;
    public required int Gender { get; set; } // 0 for male 1 for female
    public int? Budget { get; set; } = -1;
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }

}
