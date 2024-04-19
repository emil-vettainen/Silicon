using Business.Dtos.User;
using Infrastructure.Entities.AccountEntites;
using System.Diagnostics;

namespace Business.Factories;

public class UserFactory
{
    public static CreateUserDto ToDto(string firstName, string lastName, string email, string password)
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
		catch (Exception ex)
		{
            Debug.WriteLine(ex.Message);
            return null!;
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
                IsExternalAccount = isExternal,
                Created = DateTime.Now,
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }


    public static UpdateUserDto UpdateUserDto(string firstName, string lastName, string email, string phoneNumber, string biography, bool isExternalAccount)
    {
        try
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}