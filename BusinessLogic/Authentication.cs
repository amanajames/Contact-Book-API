using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTOS;
using Models.DTOS.Mapping;
using System;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserResponseDTO> Login(UserRequest userRequest)
        {
            User user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequest.Password))
                {
                    var response = UserMappings.GetUserResponse(user);
                    response.Token = await _tokenGenerator.GenerateToken(user);

                    return response;
                }
                throw new AccessViolationException("Invalid Credential");
            }

            throw new AccessViolationException("Invalid Credential");
        }

       
    }
}
