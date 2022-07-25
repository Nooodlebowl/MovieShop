﻿using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService: ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int castId)
        {
            var castDetails = await _castRepository.GetById(castId);
            var castDetailsModel = new CastDetailsModel
            {
                Id = castDetails.Id,
                Name = castDetails.Name,
                Gender = castDetails.Gender,
                TmdbUrl = castDetails.TmdbUrl,
                ProfilePath = castDetails.ProfilePath

            };
            foreach (var movie in castDetails.MoviesOfCast)
            {
                castDetailsModel.Movies.Add(new MovieModel {
                    Id = movie.MovieId,
                    Name = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl});
            }
            return castDetailsModel;
        }

    }
}
