using System.Text.Json.Serialization;

namespace Projeto_WEBAPI_CalebeBertoluci.Dto
{
    public class MoviesPatchDto
    {
        [JsonPropertyName("genres")]
        public string Genres { get; set; }

        public MoviesPatchDto(string genres)
        {
            Genres = genres;
        }
    }
}