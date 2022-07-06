namespace CleanArchMvc.Domain.Account
{
    public interface IAutheticate
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> RegisterUser(string email, string password);
        Task Logout();
    }
}