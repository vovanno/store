namespace OnlineStoreApi.SubGenreApi
{
    public class CreateSubGenreRequest
    {
        public string Name { get; set; }
        public int GenreId { get; set; }
    }

    public class EditSubGenreRequest
    {
        public string Name { get; set; }
        public int GenreId { get; set; }
    }
}
