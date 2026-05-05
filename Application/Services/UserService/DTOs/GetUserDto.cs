using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.UserService.DTOs
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
    }
}
