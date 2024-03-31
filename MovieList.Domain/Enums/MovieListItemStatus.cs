using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Domain.Enums
{   
    public enum MovieListItemStatus
    {
        Watched = 1,
        Watching = 2,
        WantToWatch = 3,
        Stalled = 4,
        Dropped = 5
    }
}
