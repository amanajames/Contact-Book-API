using Models.DTOS;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IAuthentication
    {
        Task<UserResponseDTO> Login(UserRequest userRequest);
        
    }
}