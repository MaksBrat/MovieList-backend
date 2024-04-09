using MovieList.Domain.Entity.Profile;

namespace MovieList.Domain.Entity
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
