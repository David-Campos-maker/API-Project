using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Game;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll() {

            var games = await _context.Games.ToListAsync();
            var gamesDto = games.Select(game => game.ToGameDto());

            return Ok(gamesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var game = await _context.Games.FindAsync(id);

            if (game == null) return NotFound();

            return Ok(game.ToGameDto()); 
        }

        [HttpPost]
        [Route("addGame")]
        public async Task<IActionResult> CreateNewGame([FromBody] CreateGameRequestDto requestDto) {
            var gameModel = requestDto.ToGameFromCreateDtoRequest();

            await _context.Games.AddAsync(gameModel);
            await _context.SaveChangesAsync();

            // Returns the new Game object using the GetById endpoint
            return CreatedAtAction(nameof(GetById) , new { id = gameModel.Id } , gameModel.ToGameDto());
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateGameRequestDto updateRequestDto) {
            var gameModel = await _context.Games.FirstOrDefaultAsync(game => game.Id == id);

            if (gameModel == null) return NotFound();

            // Tracking and updating the game register 
            gameModel.Name = updateRequestDto.Name;
            gameModel.CoverPhoto = updateRequestDto.CoverPhoto;
            gameModel.Published = updateRequestDto.Published;
            gameModel.Platform = updateRequestDto.Platform;
            gameModel.Gender = updateRequestDto.Gender;

            await _context.SaveChangesAsync();

            return Ok(gameModel.ToGameDto());
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var gameModel = await _context.Games.FirstOrDefaultAsync(game => game.Id == id);

            if (gameModel == null) return NotFound();

            _context.Games.Remove(gameModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}