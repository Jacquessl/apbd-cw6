using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO_s;

public class AddAnimal
{
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
    [MaxLength(200)]
    public string Category { get; set; }
    [MaxLength(200)]
    public string Area { get; set; }
}