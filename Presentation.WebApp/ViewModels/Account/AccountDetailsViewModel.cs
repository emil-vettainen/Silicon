using Presentation.WebApp.Models.Account;

namespace Presentation.WebApp.ViewModels.Account;

public class AccountDetailsViewModel
{
    public bool IsExternalAccount { get; set; }

    public BasicInfoModel BasicInfo { get; set; } = null!;

    public AddressInfoModel? AddressInfo { get; set; }

}