using Models;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}