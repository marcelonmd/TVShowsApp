using System.Text.Json.Serialization;

namespace TVShows.Models
{
    public class Show
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("genres")]
        public List<string>? Genres { get; set; }

        [JsonPropertyName("image")]
        public Image? Image { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("rating")]
        public Rating? Rating { get; set; }
    }

    public class Rating
    {
        [JsonPropertyName("average")]
        public double? Average { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("medium")]
        public string? Medium { get; set; }

        [JsonPropertyName("original")]
        public string? Original { get; set; }
    }
}