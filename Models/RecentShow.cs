namespace TVShows.Models
{
    public class CachedShow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? MediumImage { get; set; }
        public double? RatingAverage { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
        public DateTime LastViewed { get; set; }
    }
}
