namespace Projeto_WEBAPI_CalebeBertoluci.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Movies
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("year")]
    public string Year { get; set; }

    [JsonPropertyName("imdbrating")]
    public string ImdbRating { get; set; }

    [JsonPropertyName("movieposter")]
    public string MoviePoster { get; set; }

    [JsonPropertyName("genres")]
    public string Genres { get; set; }

    [JsonPropertyName("directors")]
    public string Directors { get; set; }

    [JsonPropertyName("cast")]
    public string Casts { get; set; }

    [JsonPropertyName("similar_movies")]
    public string SimilarMovie { get; set; }
    
    public Movies(int id, string name, string year, string imdbRating, string moviePoster, string genres, string directors, string cast, string similar_movies)
    {
        Id = id;
        Name = name;
        Year = year;
        ImdbRating = imdbRating;
        MoviePoster = moviePoster;
        Genres = genres;
        Directors = directors;
        Casts = cast;
        SimilarMovie = similar_movies;
    }
    
    public Movies clone()
    {
        return (Movies)this.MemberwiseClone();
    }
}