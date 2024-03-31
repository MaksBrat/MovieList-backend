using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Domain.RequestModels.Genre
{
    public class GenreRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
