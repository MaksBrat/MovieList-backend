namespace MovieList.BLL.Models.DTO.Filters
{
    public class GenreFilterRequest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool? Checked { get; set; }
    }
}
