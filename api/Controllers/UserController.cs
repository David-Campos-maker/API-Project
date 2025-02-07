using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.User;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

// Always Similar

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("getUserById")]
        [ProducesResponseType(typeof(UserDto) , 200)]
        public IActionResult GetById ([FromBody] int id) {
            var user = _context.Users.Find(id);

            if (user == null) return NotFound();

            return Ok(user.ToUserDto());
        }
    }
}