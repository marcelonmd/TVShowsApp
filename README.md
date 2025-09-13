# TV Shows App

TV Shows App is an interactive **Blazor Server** web application that consumes the public **TVMaze API** to display TV show information, seasons, and episodes.

It includes features such as:

* Viewing shows by page.
* Searching shows by name (single and multiple results).
* Managing user favorites.
* Tracking recently viewed shows.
* Internationalization support: English, Portuguese, Spanish, and French.
* Consistent genre color coding for background and border.
* Unit tests covering services consuming the API.

---

## Technologies

* .NET 8 / Blazor Server
* C#
* Moq & xUnit (unit testing in separate project `TVShows.Tests`)
* TVMaze API (data consumption)
* `IJSRuntime` (localStorage for favorites & recent shows)
* `.resx` files for internationalization
* Tailwind / Custom CSS for cards and genre colors

---

## Features

* Infinite scroll TV show listing.
* Show search by name with multi-result suggestion.
* Show details, including seasons, with episodes as next improvement.
* Favorites: add, remove, and persist locally via `localStorage`.
* Recently viewed shows history with a limit of 50 entries.
* Internationalization (i18n) and multi-language support.
* Responsive layout and consistent cards with visible icons and ratings.

---

## Project Structure

```
/TVShows
├─ Components/          # Blazor components (Cards, Layouts, etc.)
├─ Models/              # Domain classes (Show, Season, Episode, FavoriteShow, CachedShow)
├─ Services/            # Services: TvMazeService, FavoriteService, RecentShowsService
├─ Utils/               # Helper classes (GenreType, GenreColors)
├─ Resources/           # .resx files for localization
├─ Program.cs           # DI, HttpClient, and localization configuration
├─ TVShows.csproj
/TVShows.Tests
├─ Services/            # Unit tests for services
│  └─ TvMazeServiceTests.cs
├─ TVShows.Tests.csproj
```

---

## Running Locally

1. Clone this repository:

```bash
git clone https://github.com/marcelonmd/TVShowsApp.git
cd tvshows-app
```

2. Open the solution in **Visual Studio 2022+** or **VS Code**.

3. Restore NuGet packages:

```bash
dotnet restore
```

4. Run the application:

```bash
dotnet run --project TVShows
```

5. Open your browser and navigate to:

```
https://localhost:7105
```

---

## Running Tests

The tests are located in a separate project `TVShows.Tests`. They primarily cover `TvMazeService`:

```bash
dotnet test TVShows.Tests/TVShows.Tests.csproj
```

Tests use **Moq** to mock `HttpClient` responses and **xUnit** for assertions.

---

## Internationalization

* **Default language:** English
* **Supported languages:**

  * `en` - English
  * `pt` - Portuguese
  * `es` - Spanish
  * `fr` - French

Translations are managed via `.resx` files and can be easily expanded.

---

## Logging and Code Standards

* Service logs are in **English** for professional consistency.
* Services (`FavoriteService` and `RecentShowsService`) use `IJSRuntime` to persist data in `localStorage`.
* Unit tests are in a separate test project (`TVShows.Tests`) for maintainability.

---

## Next Steps / Improvements

* List episodes of each season with rating and summary.
* Enhanced UI/UX with animations for cards.
* User login.
* More unit tests for `FavoriteService` and `RecentShowsService`.

---

## Preview

* Shows (with search)
<img width="1702" height="905" alt="image" src="https://github.com/user-attachments/assets/bf776ba0-5c0b-4fba-bc30-0d07766ba7e9" />


* Show Detail:
<img width="1801" height="750" alt="image" src="https://github.com/user-attachments/assets/4bea1359-75dc-468f-a6b4-66ac20271cc0" />


* Favorite Shows:
<img width="1834" height="770" alt="image" src="https://github.com/user-attachments/assets/fb868c3a-7000-4961-8084-4acc16ee65c4" />


---

## License

This project is licensed under the **MIT License**. See `LICENSE` for details.
