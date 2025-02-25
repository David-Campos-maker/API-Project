using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Game;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) {

            var games = await _gameRepository.GetAllGamesAsync(query);
            var gamesDto = games.Select(game => game.ToGameDto());

            return Ok(gamesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var game = await _gameRepository.GetGameByIdAsync(id);

            if (game == null) return NotFound();

            return Ok(game.ToGameDto()); 
        }

        [HttpPost]
        [Route("addGame")]
        public async Task<IActionResult> CreateNewGame([FromBody] CreateGameRequestDto requestDto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var gameModel = requestDto.ToGameFromCreateDtoRequest();

            await _gameRepository.AddGameAsync(gameModel);

            // Returns the new Game object using the GetById endpoint
            return CreatedAtAction(nameof(GetById) , new { id = gameModel.Id } , gameModel.ToGameDto());
        }

        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateGameRequestDto updateRequestDto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var gameModel = await _gameRepository.UpdateGameAsync(id , updateRequestDto);

            if (gameModel == null) return NotFound();

            return Ok(gameModel.ToGameDto());
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var gameModel = await _gameRepository.DeleteGameByIdAsync(id);

            if (gameModel == null) return NotFound();

            return NoContent();
        }
    }
}