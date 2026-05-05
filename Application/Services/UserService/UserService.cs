using Application.Repositories;
using Application.Services.UserService.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(CreateUserDto input)
        {
            if (await _userRepository.GetAll().AnyAsync(x => x.Email.ToLower().Trim() == input.Email.ToLower().Trim()))
                throw new Exception("Registration failed: email or phone number already exists.");

            if (await _userRepository.GetAll().AnyAsync(x => x.PhoneNumber.Trim() == input.PhoneNumber.Trim()))
                throw new Exception("Registration failed: email or phone number already exists.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Email = input.Email.ToLower().Trim(),
                PhoneNumber = input.PhoneNumber.Trim(),
                RoleId = input.RoleId,
            };

            // Password Encryption
            var PasswordHasher = new PasswordHasher<User>();  // Install Microsoft.Extensions.Identity.Core
            user.Password = PasswordHasher.HashPassword(user, input.Password);

            await _userRepository.InsertAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid id)
        {
            var data = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("User was not found.");

            _userRepository.Delete(data);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<List<GetAllUsersDto>> GetAllUsers(string? name, string? email)
        {
            name = !String.IsNullOrEmpty(name) ? name.ToLower().Trim() : null;
            email = !String.IsNullOrEmpty(email) ? email.ToLower().Trim() : null;

            var users = _userRepository.GetAll();

            if (name != null)
                users = users.Where(x => x.Name.ToLower().Trim().Contains(name));

            if (email != null)
                users = users.Where(x => x.Email.ToLower().Trim().Contains(email));

            users = users.Include(x => x.Role);

            var result = users.Select(data => new GetAllUsersDto
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                RoleName = data.Role.Name,
            }).ToList();

            return result;
        }

        public async Task<GetUserDto> GetUserById(Guid id)
        {
            var data = await _userRepository.GetAll().Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("User was not found.");

            var result = new GetUserDto
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                RoleName = data.Role.Name,
            };
            return result;
        }

        public async Task UpdateUser(Guid id, UpdateUserDto input)
        {
            if( await _userRepository.GetAll().AnyAsync(x=> x.Email == input.Email && x.Id != id))
                throw new Exception("Registration failed: email or phone number already exists.");

            if(await _userRepository.GetAll().AnyAsync(x=>x.PhoneNumber == input.PhoneNumber && x.Id !=id))
                throw new Exception("Registration failed: email or phone number already exists.");

            var data = await _userRepository.GetByIdAsync(id);

            if(data == null)
                throw new Exception("User was not found.");

            data.Name = input.Name;
            data.Email = input.Email;
            data.PhoneNumber = input.PhoneNumber;
            data.RoleId = input.RoleId;

            _userRepository.Update(data);
            await _userRepository.SaveChangesAsync();
        }

    }
}
