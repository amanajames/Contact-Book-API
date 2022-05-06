using Models.DTOS;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IUserService
    {
        Task<bool> DeleteUser(string userId);
        Task<UserResponseDTO> GetUser(string userId);
        Task<UserResponseDTO> GetUserByEmail(string email);
        Task<UserResponseDTO> Register(RegistrationRequest registrationRequest);
        Task<bool> Update(string userId, UpdateUserRequest updateUser);
    }
}