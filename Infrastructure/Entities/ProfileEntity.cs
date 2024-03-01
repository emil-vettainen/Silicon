using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class ProfileEntity
{
    [Key]
    [ForeignKey(nameof(AccountEntity))]
    public Guid AccountId { get; set; }
    public virtual AccountEntity Account { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }
    public string? ProfileImageUrl { get; set; }

}