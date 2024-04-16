using MovieList.Services.Exceptions.Base;
using MovieList.Services.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace MovieList.BLL.Utilities
{
    public class Deserializer
    {
        public static T Deserialize<T>(string content)
        {
            var response = JsonConvert.DeserializeObject<T>(content);

            if (response == null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.InternalServerError,
                    "Failed to deserialize content");
            }

            return response;
        }
    }
}
