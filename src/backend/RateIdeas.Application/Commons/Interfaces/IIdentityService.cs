namespace RateIdeas.Application.Commons.Interfaces;

public interface IIdentityService
{
    public string LogIn(string userNameOrEmail, string password);
}