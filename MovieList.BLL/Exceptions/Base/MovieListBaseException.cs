
namespace MovieList.Services.Exceptions.Base
{
    public class MovieListBaseException : Exception
    {
        public string ErrorId { get; }

        public MovieListBaseException(string errorId, string message)
            : this(errorId, message, null)
        {
        }

        public MovieListBaseException(string errorId, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorId = errorId;
        }
    }
}
