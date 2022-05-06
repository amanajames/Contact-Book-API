using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Mapping
{
    public class UserMappings
    {
        public static UserResponseDTO GetUserResponse(User user)
        {
            return new UserResponseDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id
            };
        }
        public static User GetUser(RegistrationRequest request)
        {
            return new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = String.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName
            };
        }
    }
}
