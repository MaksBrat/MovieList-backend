using MovieList.Core.Interfaces;
using MovieList.Domain.DTO.Tmdb;
using MovieList.Services.Exceptions.Base;
using MovieList.Services.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using MovieList.Domain.Enums;

namespace MovieList.Core.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly RestClient _client;
        private readonly string _apiKey; // TODO: take from config

        public TmdbService()
        {
            _client = new RestClient("https://api.themoviedb.org/3");
            _apiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyNTU4NWQxN2U2MjY0MTc5MDY1MWZkMDRlM2UzMzU3NyIsInN1YiI6IjY2MGM1NWNiOTVjZTI0MDE3ZDZlMjNhZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.4xrdtXTwE5JBfJX0esvbWfCNTxnilRUnELWyn6SCjdQ";
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
                return Deserialize(response.Content);
            }
            else
            {
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.InternalServerError,
                    "Failed to fetch data from TMDB API.");
            }
        }

        // TODO: generic ?
        private TmdbMovieResponse Deserialize(string content)
        {
            var response = JsonConvert.DeserializeObject<TmdbMovieResponse>(content);

            if (response == null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.InternalServerError,
                    "Failed to deserialize media from TMDB API.");
            }

            return response;
        }
    }
}
