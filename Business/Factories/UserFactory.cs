using Business.Dtos;
using Business.Dtos.User;
using Business.Models;

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


    public static BasicInfoModel



}
