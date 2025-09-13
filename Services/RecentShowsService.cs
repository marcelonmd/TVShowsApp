using System.Text.Json;
using Microsoft.JSInterop;
using TVShows.Models;

namespace TVShows.Services
{
    public class RecentShowsService
    {
        private readonly IJSRuntime _jsRuntime;
        private List<CachedShow> _recentShows = new List<CachedShow>();
        private const string CacheFileName = "recentShows.json";
        private const int MaxCacheSize = 50;

        public RecentShowsService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _ = LoadRecentShowsAsync();
        }

        public async Task<List<CachedShow>> GetRecentShowsAsync()
        {
            if (_recentShows.Count == 0)
            {
                await LoadRecentShowsAsync();
            }
            Console.WriteLine($"[RecentShowsService] Returning {_recentShows.Count} recent shows (sorted).");
            return _recentShows.OrderByDescending(s => s.LastViewed).ToList();
        }

        public async Task AddShowToRecentAsync(Show show)
        {
            if (show == null)
            {
                Console.WriteLine("[RecentShowsService] Attempted to add a null show to recent history.");
                return;
            }

            if (!_recentShows.Any())
            {
                await LoadRecentShowsAsync();
            }

            var existingShow = _recentShows.FirstOrDefault(s => s.Id == show.Id);

            if (existingShow != null)
            {
                existingShow.LastViewed = DateTime.UtcNow;
                Console.WriteLine($"[RecentShowsService] Show '{show.Name}' ({show.Id}) already exists, updated LastViewed to {existingShow.LastViewed}.");
            }
            else
            {
                var newCachedShow = new CachedShow
                {
                    Id = show.Id,
                    Name = show.Name,
                    MediumImage = show.Image?.Medium,
                    RatingAverage = show.Rating?.Average,
                    Genres = show.Genres?.ToList() ?? new List<string>(),
                    LastViewed = DateTime.UtcNow
                };
                _recentShows.Add(newCachedShow);
                Console.WriteLine($"[RecentShowsService] Added new show '{newCachedShow.Name}' ({newCachedShow.Id}) to recent history. Total: {_recentShows.Count}");

                if (_recentShows.Count > MaxCacheSize)
                {
                    _recentShows = _recentShows.OrderByDescending(s => s.LastViewed)
                                               .Take(MaxCacheSize)
                                               .ToList();
                    Console.WriteLine($"[RecentShowsService] Cache exceeded {MaxCacheSize}, removed oldest shows. New total: {_recentShows.Count}");
                }
            }
            await SaveRecentShowsAsync();
        }

        public async Task RemoveShowFromRecentAsync(int showId)
        {
            if (_recentShows.Count == 0)
            {
                await LoadRecentShowsAsync();
            }

            var removedCount = _recentShows.RemoveAll(s => s.Id == showId);
            if (removedCount > 0)
            {
                Console.WriteLine($"[RecentShowsService] Removed {removedCount} show(s) with ID {showId} from recent history.");
                await SaveRecentShowsAsync();
            }
            else
            {
                Console.WriteLine($"[RecentShowsService] No show with ID {showId} found to remove from recent history.");
            }
        }

        private async Task LoadRecentShowsAsync()
        {
            Console.WriteLine("[RecentShowsService] Starting to load recent shows from localStorage.");
            try
            {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CacheFileName);
                if (!string.IsNullOrEmpty(json))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var favorites = JsonSerializer.Deserialize<List<CachedShow>>(json, options);
                    if (favorites != null)
                    {
                        _recentShows = favorites;
                        Console.WriteLine($"[RecentShowsService] Loaded {_recentShows.Count} recent shows from localStorage.");
                    }
                    else
                    {
                        _recentShows = new List<CachedShow>();
                        Console.WriteLine("[RecentShowsService] Recent shows JSON deserialized as null, starting with empty list.");
                    }
                }
                else
                {
                    _recentShows = new List<CachedShow>();
                    Console.WriteLine("[RecentShowsService] localStorage for recent shows is empty. Starting with empty list.");
                }
            }
            catch (JsonException jEx)
            {
                Console.Error.WriteLine($"[RecentShowsService] JSON error while loading recent shows: {jEx.Message}. JSON: {await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CacheFileName)}");
                _recentShows = new List<CachedShow>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RecentShowsService] General error while loading recent shows: {ex.Message}");
                _recentShows = new List<CachedShow>();
            }
        }

        private async Task SaveRecentShowsAsync()
        {
            Console.WriteLine($"[RecentShowsService] Starting to save {_recentShows.Count} recent shows to localStorage.");
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_recentShows, options);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CacheFileName, json);
                Console.WriteLine("[RecentShowsService] Recent shows successfully saved to localStorage.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RecentShowsService] Error saving recent shows: {ex.Message}");
            }
        }
    }
}
