using MovieList.Core.Interfaces;
using MovieList.Domain.DTO.Tmdb;
using MovieList.Services.Exceptions.Base;
using MovieList.Services.Exceptions;
using RestSharp;
using System.Net;
using MovieList.Domain.Enums;
using Microsoft.Extensions.Configuration;
using MovieList.BLL.Utilities;

namespace MovieList.Core.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly RestClient _client;
        private readonly IConfiguration _configuration;
        
        private readonly string _apiKey;
        private const string API_ROUTE = "https://api.themoviedb.org/3";

        public TmdbService(IConfiguration configuration)
        {
            _client = new RestClient(API_ROUTE);
            _configuration = configuration;

            _apiKey = _configuration["Tmdb:ApiKey"];
        }

        public async Task<TmdbMovieResponse> GetMediaAsync(MovieType type, string query)
        {
            var response = await GetMediaInternalAsync(type, query);

            if (response.Results == null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.InternalServerError,
                    "Failed to fetch data from TMDB API.");
            }

            return response;
        }

        private async Task<TmdbMovieResponse> GetMediaInternalAsync(MovieType type, string query, int page = 1)
        {
            var request = new RestRequest($"search/{type.ToString().ToLower()}", Method.Get)
                .AddParameter("api_key", _apiKey)
                .AddParameter("query", query)
                .AddParameter("include_adult", "false")
                .AddParameter("language", "en-US")
                .AddParameter("page", page);

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful && response.Content != null)
            {
                return Deserializer.Deserialize<TmdbMovieResponse>(response.Content);
            }
            else
            {
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.InternalServerError,
                    "Failed to fetch data from TMDB API.");
            }
        }
    }
}
