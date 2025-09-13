namespace TVShows.Models
{
    public class Season
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public int? EpisodeOrder { get; set; }
        public string? PremiereDate { get; set; }
        public string? EndDate { get; set; }
        public Image? Image { get; set; }
        public string? Summary { get; set; }
    }
}