using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Game;
using api.Helpers;
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

        public async Task<List<Games>> GetAllGamesAsync(QueryObject query)
        {
            var games = _context.Games.Include(game => game.Reviews).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name)) 
                games = games.Where(g => g.Name.Contains(query.Name));

            if (!string.IsNullOrWhiteSpace(query.Gender)) 
                games = games.Where(g => g.Gender.Contains(query.Gender));
                
            if (!string.IsNullOrWhiteSpace(query.SortBy)) {
                switch (query.SortBy) {
                    case var sortValue when 
                    string.Equals(sortValue , "Name" , StringComparison.OrdinalIgnoreCase):
                        games = query.IsDescending ? games.OrderByDescending(g => g.Name)
                        : games.OrderBy(g => g.Name);

                        break;

                    case var sortValue when
                    string.Equals(sortValue , "Published" , StringComparison.OrdinalIgnoreCase):
                        games = query.IsDescending ? games.OrderByDescending(g => g.Published.Year)
                        : games.OrderBy(g => g.Published.Year);

                        break;

                    default:
                        break;
                }
            }

            // Pagination
            var skipNunber = (query.PageNumber - 1) * query.PageSize;

            return await games.Skip(skipNunber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Games?> GetGameByIdAsync(int id)
        {
            return await _context.Games.Include(game => game.Reviews).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Games> AddGameAsync(Games game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<Games?> UpdateGameAsync(int id, UpdateGameRequestDto updateGameDto)
        {
            var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

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
            var gameModel = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (gameModel == null) return null;

            _context.Games.Remove(gameModel);
            await _context.SaveChangesAsync();

            return gameModel;
        }

        public async Task<bool> IsGameExist(int id)
        {
            return await _context.Games.AnyAsync(g => g.Id == id);
        }
    }
}