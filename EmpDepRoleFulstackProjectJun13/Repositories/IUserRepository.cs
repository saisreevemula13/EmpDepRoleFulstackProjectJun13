
using EmpDepRoleFulstackProjectJun13.Models.Domain;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public interface IUserRepository
    {
      Task<User?> ValidateUserAsync(string username, string password);
    }
}
