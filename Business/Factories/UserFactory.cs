using Business.Dtos;
using Business.Dtos.User;
using Infrastructure.Entities;

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


    public static GetUserDto GetUser(string firstName, string lastName, string email, string phone, string bio, string imgUrl)
    {
        try
        {
            return new GetUserDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phone,
                Biography = bio,
                
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


    public static UpdateUserDto UpdateUserDto(string firstName, string lastName, string email, string phoneNumber, string biography)
    {
        return new UpdateUserDto
        {
            UserName = email,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Biography = biography

        };
    }



}
