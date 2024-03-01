using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AccountEntity : IdentityUser
{

    [ForeignKey(nameof(AddressEntity))]
    public int? AddressId { get; set; }

    public virtual AddressEntity Address { get; set; } = null!;
   
    public virtual ProfileEntity Profile { get; set; } = null!;

}