using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTOS;
using Models.DTOS.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<bool> Update(string userId, UpdateUserRequest updateUser)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = string.IsNullOrWhiteSpace(updateUser.FirstName) ? user.FirstName : updateUser.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(updateUser.LastName) ? user.LastName : updateUser.LastName;
                user.PhoneNumber = string.IsNullOrWhiteSpace(updateUser.PhoneNumber) ? user.PhoneNumber : updateUser.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                throw new MissingMemberException(errors);
            }

            throw new ArgumentException("Resourse not found");
        }
        public async Task<UserResponseDTO> Register(RegistrationRequest registrationRequest)
        {
            User user = UserMappings.GetUser(registrationRequest);

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                return UserMappings.GetUserResponse(user);
            }
            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingFieldException(errors);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                throw new MissingMemberException(errors);
            }

            throw new ArgumentException("Resourse not found");

        }

        public async Task<UserResponseDTO> GetUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return UserMappings.GetUserResponse(user);
            }

            throw new ArgumentException("Resourse not found");
        }

        public async Task<UserResponseDTO> GetUserByEmail(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return UserMappings.GetUserResponse(user);
            }

            throw new ArgumentException("Resourse not found");
        }
    }
}
