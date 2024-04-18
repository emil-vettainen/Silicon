namespace Presentation.WebApp.Helpers;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UserSessionValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserSessionValidationMiddleware>();
    }
}