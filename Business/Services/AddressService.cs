using Business.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Business.Services;

public class AddressService
{


	//private readonly AddressRepository _addressRepository;

 //   public AddressService(AddressRepository addressRepository)
 //   {
 //       _addressRepository = addressRepository;
 //   }

 //   public async Task<bool> CreateOneAddressAsync(string userId, AddressDto dto)
 //   {

	//	try
	//	{
	//		var createdAddress = await _addressRepository.CreateAsync(new AddressEntity
	//		{
	//			UserId = userId,
	//			StreetName = dto.Address_1,
	//			SecondStreetName = dto.Address_2,
	//			PostalCode = dto.PostalCode,
	//			City = dto.City,
	//		});

	//		return createdAddress != null;

	//	}
	//	catch (Exception)
	//	{

			
	//	}

	//	return false;

 //   }



 //   public async Task<bool> UpdateAddressEntityAsync(string userId, AddressDto dto)
 //   {
 //       try
 //       {
 //           var newAddressEntity = await _addressRepository.UpdateAsync(x => x.UserId == userId, new AddressEntity
 //           {
 //               UserId = userId,
 //               StreetName = dto.Address_1,
 //               SecondStreetName = dto.Address_2,
 //               PostalCode = dto.PostalCode,
 //               City = dto.City,
 //           });
 //           return newAddressEntity != null;

 //       }
 //       catch (Exception)
 //       {


 //       }
 //       return false;
 //   }


    //public async Task<bool> GetOneAddress(string userId)
    //{
    //    try
    //    {
    //        var address
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

}
