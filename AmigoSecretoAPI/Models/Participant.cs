using System.ComponentModel.DataAnnotations;

namespace AmigoSecretoAPI.Models;

public class Participant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
}
