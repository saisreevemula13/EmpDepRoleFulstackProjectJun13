using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public class AuthService:IAuthService
    {
            private readonly IUserRepository _userRepository;

            public AuthService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<User?> ValidateUserAsync(string username, string password)
            {
                // Now delegate DB logic to repository layer
                return await _userRepository.ValidateUserAsync(username, password);
            }
    }
}
