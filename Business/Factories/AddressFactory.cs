﻿using Business.Dtos;
using Infrastructure.Entities;

namespace Business.Factories;

public class AddressFactory
{
    public static OptionalAddressEntity CreateOptionalEntity (string optionalAddress)
    {
		try
		{
			return new OptionalAddressEntity
			{
				OptionalAddress = optionalAddress,
			};
		}
		catch (Exception)
		{

			return null!;
		}
    }


	public static AddressDto GetAddressDto (string streetName, string? optionalAddress, string postalCode, string city)
	{
		try
		{
			return new AddressDto
			{
				StreetName = streetName,
				OptionalAddress = optionalAddress,
				PostalCode = postalCode,
				City = city
			};
		}
		catch (Exception)
		{

			return null!;
		}
	}

    public static AddressDto CreateAddressDto(string streetName, string? optionalAddress, string postalCode, string city)
    {
        try
        {
            return new AddressDto
            {
                StreetName = streetName,
                OptionalAddress = optionalAddress,
                PostalCode = postalCode,
                City = city
            };
        }
        catch (Exception)
        {

            return null!;
        }
    }


    public static AddressEntity CreateAddressEntity (string streetName, string postalCode, string city)
	{
		try
		{
			return new AddressEntity
			{
				StreetName = streetName,
				PostalCode = postalCode,
				City = city
			};
		}
		catch (Exception)
		{

			return null!;
		}
	}


	public static UserAddressEntity CreateUserAddressEntity (string userId, int addressId, int? optionalAddressId)
	{
		try
		{
			return new UserAddressEntity
			{
				UserId = userId,
				AddressId = addressId,
				OptionalAddressId = optionalAddressId
			};
		}
		catch (Exception)
		{

			return null!;
		}
	}
}