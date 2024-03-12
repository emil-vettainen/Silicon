namespace Business.Dtos;

public class AddressDto
{
    public int AddressId { get; set; }
    public string Address_1 { get; set; } = null!;
    public string? Address_2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
