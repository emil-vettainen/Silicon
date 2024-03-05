using Business.Dtos;
using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.ViewModels;

public class AccountDetailsViewModel
{

    public BaseInfoModel BaseInfo { get; set; } = new();

    public AddressInfoModel? AddressInfo { get; set; } = new();
}
