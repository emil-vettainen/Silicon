using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class SecondAddressEntity
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string StreetName_2 { get; set; } = null!;

    public virtual ICollection<AddressEntity> Addresses { get; set; } = [];
}
