using TVShows.Models;

namespace TVShows.Services
{
    public class TvMazeService : ITvMazeService
    {
        private readonly HttpClient _http;

        public TvMazeService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Show>> GetShowsByPageAsync(int page)
        {
            try
            {
                var shows = await _http.GetFromJsonAsync<List<Show>>($"shows?page={page}") ?? new List<Show>();
                Console.WriteLine($"[TvMazeService] Loaded {shows.Count} shows for page {page}.");
                return shows;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[TvMazeService] Error loading shows for page {page}: {ex.Message}");
                return new List<Show>();
            }
        }

        public async Task<Show?> GetShowByIdAsync(int id)
        {
            try
            {
                var show = await _http.GetFromJsonAsync<Show>($"shows/{id}");
                if (show != null)
                    Console.WriteLine($"[TvMazeService] Loaded show '{show.Name}' (ID: {id}).");
                else
                    Console.WriteLine($"[TvMazeService] Show with ID {id} not found.");
                return show;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[TvMazeService] Error loading show with ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Show>> SearchShowsByNameAsync(string query)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<List<SearchResult>>($"search/shows?q={Uri.EscapeDataString(query)}");
                var shows = response?.Select(r => r.Show).ToList() ?? new List<Show>();
                Console.WriteLine($"[TvMazeService] Found {shows.Count} shows for search query '{query}'.");
                return shows;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[TvMazeService] Error searching shows for query '{query}': {ex.Message}");
                return new List<Show>();
            }
        }

        public async Task<Show?> SearchShowByNameAsync(string query)
        {
            try
            {
                var show = await _http.GetFromJsonAsync<Show>($"singlesearch/shows?q={Uri.EscapeDataString(query)}");
                if (show != null)
                    Console.WriteLine($"[TvMazeService] Found show '{show.Name}' for single search query '{query}'.");
                else
                    Console.WriteLine($"[TvMazeService] No show found for single search query '{query}'.");
                return show;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[TvMazeService] Error performing single search for query '{query}': {ex.Message}");
                return null;
            }
        }

        public async Task<List<Season>> GetSeasonsByShowIdAsync(int id)
        {
            try
            {
                var seasons = await _http.GetFromJsonAsync<List<Season>>($"shows/{id}/seasons") ?? new List<Season>();
                Console.WriteLine($"[TvMazeService] Loaded {seasons.Count} seasons for show ID {id}.");
                return seasons;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[TvMazeService] Error loading seasons for show ID {id}: {ex.Message}");
                return new List<Season>();
            }
        }

        public async Task<List<Episode>> GetEpisodesBySeasonIdAsync(int id)
        {
            try
            {
                var episodes = await _http.GetFromJsonAsync<List<Episode>>($"seasons/{id}/episodes") ?? new List<Episode>();
                Console.WriteLine($"[TvMazeService] Loaded {episodes.Count} episodes for season ID {id}.");
                return episodes;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[TvMazeService] Error loading episodes for season ID {id}: {ex.Message}");
                return new List<Episode>();
            }
        }
    }

    public class SearchResult
    {
        public Show Show { get; set; }
    }
}
