using EmpDepRoleFulstackProjectJun13.Models.Domain;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(string username, string password);
    }
}
