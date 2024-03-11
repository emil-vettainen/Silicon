﻿using Presentation.WebApp.Models.AccountSecurity;

namespace Presentation.WebApp.ViewModels.Account;

public class SecurityViewModel
{
    public bool IsExternalAccount { get; set; }
    public ChangePasswordModel ChangePassword { get; set; } = null!;
    public DeleteAccountModel DeleteAccount { get; set; } = null!;
}
