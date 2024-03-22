using Business.Dtos.Address;
using Business.Factories;
using Infrastructure.Repositories.SqlRepositories;
using Shared.Factories;
using Shared.Responses;

namespace Business.Services;

public class AddressService
{

	private readonly AddressRepository _addressRepository;
    private readonly UserAddressRepository _userAddressRepository;
    private readonly OptionalAddressRepository _optionalAddressRepository;


    public AddressService(AddressRepository addressRepository, UserAddressRepository userAddressRepository, OptionalAddressRepository optionalAddressRepository)
    {
        _addressRepository = addressRepository;
        _userAddressRepository = userAddressRepository;
        _optionalAddressRepository = optionalAddressRepository;
    }



    public async Task<AddressDto> GetAddressInfoAsync(string userId)
    {
        try
        {
            var result = await _userAddressRepository.GetAllAddressesAsync(userId);
            if (result != null)
            {
                var dto = AddressFactory.GetAddressDto(result.Address.StreetName, result.OptionalAddress?.OptionalAddress, result.Address.PostalCode, result.Address.City);
                return dto;
            }
        }
        catch (Exception)
        {
            
        }
        return null!;
    }







    public async Task<ResponseResult> CreateOrUpdateAddressInfoAsync(AddressDto dto, string userId)
    {
        try
        {
            var addressId = await GetOrCreateAddressAsync(dto.StreetName, dto.PostalCode, dto.City);
            if (addressId == 0)
            {
                return ResponseFactory.Error();
            }

            var existingRelation = await _userAddressRepository.GetUserAddressAsync(userId, addressId);
            int? optionalAddressId = await HandleOptionalAddressAsync(dto.OptionalAddress);

            if (existingRelation == null)
            {
                var result = await _userAddressRepository.CreateAsync(AddressFactory.CreateUserAddressEntity(userId, addressId, optionalAddressId));
                return result != null ? ResponseFactory.Ok() : ResponseFactory.Error();
            }
            else
            {
                existingRelation.OptionalAddressId = optionalAddressId;
                var result = await _userAddressRepository.UpdateAsync(x => x.UserId == userId && x.AddressId == addressId, existingRelation);
                return result != null ? ResponseFactory.Ok() : ResponseFactory.Error();
            }
        }
        catch (Exception)
        {
            return ResponseFactory.Error("Something went wrong, please try again!");
        }
    }





    public async Task<int> GetOrCreateAddressAsync(string streetName, string postalCode, string city)
    {
        try
        {
            var address = await _addressRepository.GetOneAsync(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
            if (address != null)
            {
                return address.Id; 
            }
            else
            {
                var createdAddress = await _addressRepository.CreateAsync(AddressFactory.CreateAddressEntity(streetName, postalCode, city));
                if (createdAddress != null)
                {
                    return createdAddress.Id;
                }
            }
        }
        catch (Exception)
        {

           
        }
        return 0;
    }




    public async Task<int?> HandleOptionalAddressAsync(string? optionalAddress)
    {
        try
        {
            return optionalAddress != null ? await GetOrCreateOptionalAddressAsync(optionalAddress) : null;
        }
        catch (Exception)
        {

            return null;
        }
    }


    public async Task<int?> GetOrCreateOptionalAddressAsync(string optionalAddress)
    {
        try
        {
            var optionalAddressentity = await _optionalAddressRepository.GetOneAsync(x => x.OptionalAddress == optionalAddress);
            if (optionalAddressentity != null)
            {
                return optionalAddressentity.Id;
            }

            else
            {
                var createdOptional = await _optionalAddressRepository.CreateAsync(AddressFactory.CreateOptionalEntity(optionalAddress));
                return createdOptional.Id;
            }

       

           

        }
        catch (Exception)
        {

            return null;
        }

    }

}
