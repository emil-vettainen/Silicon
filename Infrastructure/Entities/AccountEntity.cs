using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AccountEntity : IdentityUser
{

    public virtual ProfileEntity Profile { get; set; } = null!;
    public virtual ICollection<AddressEntity> Addresses { get; set; } = [];

} 