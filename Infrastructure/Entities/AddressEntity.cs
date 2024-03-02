using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }

    public virtual ICollection<AccountEntity> Accounts { get; set; } = [];

    [Column(TypeName = "nvarchar(50)")]
    public string? StreetName_1 { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? StreetName_2 { get; set; }

    [Column(TypeName = "char(6)")]
    public string? PostalCode { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? City { get; set; }

    public int? SecondAddressId { get; set; }

    public virtual SecondAddressEntity SecondAddress { get; set; } = null!;

}