using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public GameController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {
            var games = _context.Games.ToList();

            return Ok(games);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) {
            var game = _context.Games.Find(id);

            if (game == null) return NotFound();

            return Ok(game);
        }
    }
}