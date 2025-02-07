using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Game;
using api.Models;

namespace api.Mappers
{
    public static class GameMappers
    {
        public static GameDto ToGameDto(this Games gameModel) {
            return new GameDto {
                Id = gameModel.Id,
                Name = gameModel.Name,
                CoverPhoto = gameModel.CoverPhoto,
                Published = gameModel.Published,
                Platform = gameModel.Platform,
                Gender = gameModel.Gender,
            };
        }

        public static Games ToGameFromCreateDtoRequest(this CreateGameRequestDto createGameDto) {
            return new Games {
                Name = createGameDto.Name,
                CoverPhoto = createGameDto.CoverPhoto,
                Published = createGameDto.Published,
                Platform = createGameDto.Platform,
                Gender = createGameDto.Gender,
            };
        }
    }
}