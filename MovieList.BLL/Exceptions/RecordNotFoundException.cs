using MovieList.Services.Exceptions.Base;

namespace MovieList.Services.Exceptions
{
    public class RecordNotFoundException : MovieListBaseException
    {
        public RecordNotFoundException(string errorId, string message)
            : base(errorId, message)
        {
        }
    }
}
