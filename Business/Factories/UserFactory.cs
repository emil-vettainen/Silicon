﻿using Business.Dtos;
using Business.Dtos.User;
using Infrastructure.Entities.AccountEntites;

namespace Business.Factories;

public class UserFactory
{
    public static CreateUserDto CreateUser(string firstName, string lastName, string email, string password)
    {
		try
		{
            return new CreateUserDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password

            };
        }
		catch (Exception)
		{

			throw;
		}
    }

    public static UserEntity CreateUserEntity (string firstName, string lastName, string email, bool isExternal) 
    {
        try
        {
            return new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                IsExternalAccount = isExternal
            };
        }
        catch (Exception)
        {

            throw;
        }
    }


    public static GetUserDto GetUserDto(string firstName, string lastName, string email, string phoneNumber, string biography, bool isExternalAccount)
    {
        try
        {
            return new GetUserDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Biography = biography,
                IsExternalAccount = isExternalAccount,
                
            };
        }
        catch (Exception)
        {

            return null!;
        }
    }


    public static UserEntity UpdateUser(UpdateUserDto updateUser)
    {
        try
        {
            return new UserEntity
            {
             
                UserName = updateUser.UserName,
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                PhoneNumber = updateUser.PhoneNumber,
                Biography = updateUser.Biography,

            };
        }
        catch (Exception)
        {

            throw;
        }
    }


    public static UpdateUserDto UpdateUserDto(string firstName, string lastName, string email, string phoneNumber, string biography, bool isExternalAccount)
    {
        return new UpdateUserDto
        {
            UserName = email,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Biography = biography,
            IsExternalAccount = isExternalAccount,
            

        };
    }



}
