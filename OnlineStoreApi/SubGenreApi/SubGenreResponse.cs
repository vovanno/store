using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreApi.SubGenreApi
{
    public class SubGenreResponse
    {
        public int Id { get; set; }

        public int GenreId { get; set; }

        public string Name { get; set; }
    }
}
