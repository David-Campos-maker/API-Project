using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Game;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDBContext _context;
        public GameRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Games>> GetAllGamesAsync()
        {
            return await _context.Games.Include(reviews => reviews.Reviews).ToListAsync();
        }

        public async Task<Games?> GetGameByIdAsync(int id)
        {
            return await _context.Games.Include(review => review.Reviews).FirstOrDefaultAsync(game => game.Id == id);
        }

        public async Task<Games> AddGameAsync(Games game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<Games?> UpdateGameAsync(int id, UpdateGameRequestDto updateGameDto)
        {
            var existingGame = await _context.Games.FirstOrDefaultAsync(game => game.Id == id);

            if (existingGame == null) return null;

            // Tracking and updating the game register 
            existingGame.Name = updateGameDto.Name;
            existingGame.CoverPhoto = updateGameDto.CoverPhoto;
            existingGame.Published = updateGameDto.Published;
            existingGame.Platform = updateGameDto.Platform;
            existingGame.Gender = updateGameDto.Gender;

            await _context.SaveChangesAsync();

            return existingGame;
        }

        public async Task<Games?> DeleteGameByIdAsync(int id)
        {
            var gameModel = await _context.Games.FirstOrDefaultAsync(game => game.Id == id);

            if (gameModel == null) return null;

            _context.Games.Remove(gameModel);
            await _context.SaveChangesAsync();

            return gameModel;
        }
    }
}