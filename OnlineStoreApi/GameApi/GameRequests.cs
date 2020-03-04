namespace OnlineStoreApi.GameApi
{
    public class CreateGameRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public double Price { get; set; }

        public int[] GenresIds { get; set; }

        public int[] PlatformsIds { get; set; }
    }

    public class EditGameRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public double Price { get; set; }

        public int[] GenresIds { get; set; }

        public int[] PlatformsIds { get; set; }
    }
}
