namespace MovieList.Domain.ApiError
{
    public class ApiError
    {
        public int? HttpStatusCode { get; set; }

        public string ErrorId { get; set; }

        public string Message { get; set; }

        public static ApiError Create(string errorId, string message, int? statusCode = null)
        {
            return new ApiError
            {
                HttpStatusCode = statusCode,
                ErrorId = errorId,
                Message = message
            };
        }
    }
}
