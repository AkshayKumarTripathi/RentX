using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentX.Models;

public class Property
{
    [Key]
    public int Id {  get; set; }
    public required string Address { get; set; }
    public required int RentLow { get; set; }
    public required int RentHigh {  get; set; }
    public required string City { get; set; }

    // Foreign key statement here
    [Required]
    public required int PropertyTypeId { get; set; }
    public string? PropertyName { get; set; }

    [ForeignKey("PropertyTypeId")]
    public PropertyType? PropertyType { get; set; }

    // Created by which user property
    [Required]
    public required int CreatorUserId {  get; set; }

    [ForeignKey("CreatorUserId")]
    public User? User { get; set; }

}
