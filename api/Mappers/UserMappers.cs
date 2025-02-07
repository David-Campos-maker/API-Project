using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User user) {
            return new UserDto {
                Id = user.Id,
                Name = user.Name,
                PhotoUrl = user.PhotoUrl
            };
        }
    }
}