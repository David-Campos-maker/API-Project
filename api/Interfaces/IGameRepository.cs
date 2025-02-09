using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Game;
using api.Models;

namespace api.Interfaces
{
    public interface IGameRepository
    {
        Task<List<Games>> GetAllGamesAsync();
        Task<Games?> GetGameByIdAsync(int id);
        Task<Games> AddGameAsync(Games game);
        Task<Games?> UpdateGameAsync(int id , UpdateGameRequestDto updateGameDto);
        Task<Games?> DeleteGameByIdAsync(int id);
    }
}