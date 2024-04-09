namespace MovieList.Domain.ResponseModels.Account
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }

        public int? UserId { get; set; }
    }
}
