using Business.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Business.Services;

public class AddressService
{

	private readonly AddressRepository _addressRepository;
    private readonly UserAddressRepository _userAddressRepository;


    public AddressService(AddressRepository addressRepository, UserAddressRepository userAddressRepository)
    {
        _addressRepository = addressRepository;
        _userAddressRepository = userAddressRepository;
    }



    public async Task<UserAddressEntity> GetAddressInfoAsync(string userId)
    {

        var result = await _userAddressRepository.GetAllAddressesAsync(userId);
        if (result != null)
        {
            return result;
        }
        return null!;
       

    }




    public async Task CreateOrUpdateAsync(AddressDto dto, string userId)
    {

      


        var address = await _addressRepository.GetOneAsync(x => x.StreetName == dto.Address_1 && x.PostalCode == dto.PostalCode && x.City == dto.City);

        if (address == null)
        {
            address = new AddressEntity
            {
                StreetName = dto.Address_1,
                PostalCode = dto.PostalCode,
                City = dto.City,
            };

           var result = await _addressRepository.CreateAsync(address);
        }


        var existingRelation = await _addressRepository.GetUserAddressAsync(userId, address.Id);

        if (existingRelation == null)
        {
            var newUserAddress = new UserAddressEntity
            {
                UserId = userId,
                AddressId = address.Id,
                OptionalAddress = dto.Address_2,
            };

            await _addressRepository.AddUserAddressAsync(newUserAddress);
        }
        else
        {
            existingRelation.OptionalAddress = dto.Address_2;
            await _addressRepository.UpdateUserAddressAsync(existingRelation);
            
        }


    }



}
