using Business.Dtos;

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



}
