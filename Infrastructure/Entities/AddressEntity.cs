using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(AccountEntity))]
    public string UserId { get; set; } = null!;

    public virtual AccountEntity Account { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string StreetName { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string? SecondStreetName { get; set; }

    [Column(TypeName = "char(6)")]
    public string PostalCode { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

   

}