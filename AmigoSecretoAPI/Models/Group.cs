using System.ComponentModel.DataAnnotations;

namespace AmigoSecretoAPI.Models;

public class Group
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    public List<Participant>? Participants { get; set; } = new();
    public List<(Participant, Participant)>? Matches { get; set; } = new();
}
