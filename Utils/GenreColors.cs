using System;
using System.Collections.Generic;

namespace TVShows.Utils
{
    public enum GenreType
    {
        Unknown,
        Action,
        Adventure,
        Animation,
        Comedy,
        Crime,
        Documentary,
        Drama,
        Family,
        Fantasy,
        History,
        Horror,
        Music,
        Mystery,
        Romance,
        SciFi,
        Thriller,
        War,
        Western,
        Medical,
        Travel,
        Food,
        Sports,
        Legal,
        Supernatural
    }

    public static class GenreColors
    {
        private static readonly Dictionary<GenreType, string> BackgroundColors = new Dictionary<GenreType, string>
        {
            { GenreType.Action, "#E57373" },
            { GenreType.Adventure, "#81C784" },
            { GenreType.Animation, "#64B5F6" },
            { GenreType.Comedy, "#FFF176" },
            { GenreType.Crime, "#D32F2F" },
            { GenreType.Documentary, "#90A4AE" },
            { GenreType.Drama, "#BA68C8" },
            { GenreType.Family, "#FFD54F" },
            { GenreType.Fantasy, "#9575CD" },
            { GenreType.History, "#A1887F" },
            { GenreType.Horror, "#C62828" },
            { GenreType.Music, "#F06292" },
            { GenreType.Mystery, "#5E35B1" },
            { GenreType.Romance, "#F48FB1" },
            { GenreType.SciFi, "#4DD0E1" },
            { GenreType.Thriller, "#4DB6AC" },
            { GenreType.War, "#757575" },
            { GenreType.Western, "#8D6E63" },
            { GenreType.Medical, "#26A69A" },
            { GenreType.Travel, "#FFB74D" },
            { GenreType.Food, "#A1887F" },
            { GenreType.Sports, "#388E3C" },
            { GenreType.Legal, "#42A5F5" },
            { GenreType.Supernatural, "#7E57C2" },
            { GenreType.Unknown, "#E0E0E0" }
        };

        private static readonly Dictionary<GenreType, string> BorderColors = new Dictionary<GenreType, string>
        {
            { GenreType.Action, "#D32F2F" },
            { GenreType.Adventure, "#388E3C" },
            { GenreType.Animation, "#1976D2" },
            { GenreType.Comedy, "#FBC02D" },
            { GenreType.Crime, "#B71C1C" },
            { GenreType.Documentary, "#607D8B" },
            { GenreType.Drama, "#8E24AA" },
            { GenreType.Family, "#FFA000" },
            { GenreType.Fantasy, "#673AB7" },
            { GenreType.History, "#6D4C41" },
            { GenreType.Horror, "#B71C1C" },
            { GenreType.Music, "#C2185B" },
            { GenreType.Mystery, "#4527A0" },
            { GenreType.Romance, "#AD1457" },
            { GenreType.SciFi, "#00838F" },
            { GenreType.Thriller, "#00796B" },
            { GenreType.War, "#424242" },
            { GenreType.Western, "#6D4C41" },
            { GenreType.Medical, "#00796B" },
            { GenreType.Travel, "#F57C00" },
            { GenreType.Food, "#6D4C41" },
            { GenreType.Sports, "#2E7D32" },
            { GenreType.Legal, "#1976D2" },
            { GenreType.Supernatural, "#512DA8" },
            { GenreType.Unknown, "#B0B0B0" }
        };

        public static string GetBackgroundColor(GenreType genre)
            => BackgroundColors.GetValueOrDefault(genre, BackgroundColors[GenreType.Unknown]);

        public static string GetBorderColor(GenreType genre)
            => BorderColors.GetValueOrDefault(genre, BorderColors[GenreType.Unknown]);
    }
}
