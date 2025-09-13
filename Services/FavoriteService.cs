using Microsoft.JSInterop;
using System.Text.Json;
using TVShows.Models;

namespace TVShows.Services
{
    public class FavoriteService
    {
        private readonly IJSRuntime _jsRuntime;
        private List<FavoriteShow> _favoriteShows = new List<FavoriteShow>();
        private const string CacheFileName = "favorites.json";

        public FavoriteService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _ = LoadFavoriteShowsAsync();
        }

        public async Task<List<FavoriteShow>> GetFavoriteShowsAsync()
        {
            if (!_favoriteShows.Any())
            {
                await LoadFavoriteShowsAsync();
            }
            return _favoriteShows;
        }

        public async Task AddFavoriteShowAsync(Show show)
        {
            if (!_favoriteShows.Any(f => f.Id == show.Id))
            {
                _favoriteShows.Add(new FavoriteShow
                {
                    Id = show.Id,
                    Name = show.Name,
                    MediumImage = show.Image?.Medium,
                    RatingAverage = show.Rating?.Average,
                    Genres = show.Genres?.ToList() ?? new List<string>()
                });

                await SaveFavoriteShowsAsync();
                Console.WriteLine($"Added show '{show.Name}' to favorites.");
            }
        }

        public async Task RemoveFavoriteShowAsync(int showId)
        {
            var removedCount = _favoriteShows.RemoveAll(f => f.Id == showId);
            if (removedCount > 0)
            {
                await SaveFavoriteShowsAsync();
                Console.WriteLine($"Removed show with ID {showId} from favorites.");
            }
        }

        private async Task LoadFavoriteShowsAsync()
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CacheFileName);
                if (!string.IsNullOrEmpty(json))
                {
                    var favorites = JsonSerializer.Deserialize<List<FavoriteShow>>(json);
                    if (favorites != null)
                    {
                        _favoriteShows = favorites;
                        Console.WriteLine($"Loaded {_favoriteShows.Count} favorite shows from localStorage.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading favorite shows: {ex.Message}");
            }
        }

        private async Task SaveFavoriteShowsAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(_favoriteShows);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CacheFileName, json);
                Console.WriteLine($"Saved {_favoriteShows.Count} favorite shows to localStorage.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving favorite shows: {ex.Message}");
            }
        }
    }
}
