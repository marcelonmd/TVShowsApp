using TVShows.Models;

namespace TVShows.Services
{
    public interface ITvMazeService
    {
        Task<List<Show>> GetShowsByPageAsync(int page);
        Task<Show?> GetShowByIdAsync(int id);
        Task<Show?> SearchShowByNameAsync(string query);
        Task<List<Show>> SearchShowsByNameAsync(string query);

        Task<List<Season>> GetSeasonsByShowIdAsync(int id);
        Task<List<Episode>> GetEpisodesBySeasonIdAsync(int id);
    }

}
