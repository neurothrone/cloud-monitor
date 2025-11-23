using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudMonitor.Persistence.Entities;

[Table("persons")]
public class PersonEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public required string FirstName { get; set; }

    [MaxLength(100)]
    public required string LastName { get; set; }
}