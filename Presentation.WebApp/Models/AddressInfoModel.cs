using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class AddressInfoModel
{
    [Display(Name = "Address line 1", Prompt = "Enter your address line", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string Addressline_1 { get; set; } = null!;


    [Display(Name = "Address line 2", Prompt = "Enter your second address line", Order = 1)]
    public string? Addressline_2 { get; set; }


    [Display(Name = "PostalCode", Prompt = "Enter your postal code", Order = 2)]
    [DataType(DataType.PostalCode)]
    [Required(ErrorMessage = "Last name is required")]
    public string PostalCode { get; set; } = null!;


    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    [Required(ErrorMessage = "Last name is required")]
    public string City { get; set; } = null!;
}
