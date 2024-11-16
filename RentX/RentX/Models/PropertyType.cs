using System.ComponentModel.DataAnnotations;

namespace RentX.Models;

public class PropertyType
{
    [Key]
    public required int Id { get; set; }

    [Required] // Ensures this field is not nullable
    [MaxLength(50)]
    public required string PropertyTypeName { get; set; }
}
