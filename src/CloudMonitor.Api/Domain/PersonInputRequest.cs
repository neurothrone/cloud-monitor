using System.ComponentModel.DataAnnotations;

namespace CloudMonitor.Api.Domain;

public record PersonInputRequest
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public required string Email { get; init; }
}