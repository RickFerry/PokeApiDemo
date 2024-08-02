using Newtonsoft.Json;
using PokeApiDemo.Data;
using PokeApiDemo.Models;

namespace PokeApiDemo.Services
{
    public class PokemonService(HttpClient httpClient, IFileService fileService, ApplicationDbContext context) : IPokemonService
    {
        public async Task<IEnumerable<Pokemon>> GetRandomPokemons(int count)
        {
            var pokemonList = new List<Pokemon>();
            var random = new Random();
            var tasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                var id = random.Next(1, 1008);
                tasks.Add(Task.Run(async () =>
                {
                    var response = await httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}/");
                    var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);
                    _ = pokemon?.Sprites.FrontDefault ?? string.Empty;
                    if (pokemon != null)
                    {
                        var spriteUrl = pokemon.Sprites.FrontDefault;
                        pokemon.SpriteBase64 = !string.IsNullOrEmpty(spriteUrl)
                            ? await fileService.GetBase64SpriteAsync(spriteUrl)
                            : string.Empty;
                        lock (pokemonList)
                        {
                            pokemonList.Add(pokemon);
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return pokemonList;
        }

        public async Task<Pokemon> GetPokemonById(int id)
        {
            var response = await httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}/");
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);
            _ = pokemon?.Sprites.FrontDefault ?? string.Empty;
            if (pokemon != null)
            {
                var spriteUrl = pokemon.Sprites.FrontDefault;
                pokemon.SpriteBase64 = !string.IsNullOrEmpty(spriteUrl)
                    ? await fileService.GetBase64SpriteAsync(spriteUrl)
                    : string.Empty;
            }
            return pokemon ?? throw new ArgumentNullException(nameof(pokemon), "Pokemon cannot be null");
        }

        public async Task CapturePokemon(CapturedPokemon capturedPokemon)
        {
            context.CapturedPokemons.Add(capturedPokemon);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CapturedPokemon>> GetCapturedPokemons()
        {
            return await Task.FromResult(context.CapturedPokemons.ToList());
        }
    }
}