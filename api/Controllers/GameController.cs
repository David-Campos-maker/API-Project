using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Game;
using api.Mappers;
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
        [ProducesResponseType(typeof(IEnumerable<GameDto>), 200)]
        public IActionResult GetAll() {

            var games = _context.Games.AsEnumerable()
             .Select(game => game.ToGameDto());

            return Ok(games);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GameDto) , 200)]
        public IActionResult GetById([FromRoute] int id) {
            var game = _context.Games.Find(id);

            if (game == null) return NotFound();

            return Ok(game.ToGameDto()); 
        }

        [HttpPost]
        [Route("addGame")]
        [ProducesResponseType(typeof(GameDto) , 200)]
        public IActionResult CreateNewGame([FromBody] CreateGameRequestDto requestDto) {
            var gameModel = requestDto.ToGameFromCreateDtoRequest();

            _context.Games.Add(gameModel);
            _context.SaveChanges();

            // Returns the new Game object using the GetById endpoint
            return CreatedAtAction(nameof(GetById) , new { id = gameModel.Id } , gameModel.ToGameDto());
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(typeof(GameDto) , 200)]

        public IActionResult Update([FromRoute] int id , [FromBody] UpdateGameRequestDto updateRequestDto) {
            var gameModel = _context.Games.FirstOrDefault(game => game.Id == id);

            if (gameModel == null) return NotFound();

            // Tracking and updating the game register 
            gameModel.Name = updateRequestDto.Name;
            gameModel.CoverPhoto = updateRequestDto.CoverPhoto;
            gameModel.Published = updateRequestDto.Published;
            gameModel.Platform = updateRequestDto.Platform;
            gameModel.Gender = updateRequestDto.Gender;

            _context.SaveChanges();

            return Ok(gameModel.ToGameDto());
        }
    }
}