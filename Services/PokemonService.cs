using Newtonsoft.Json;
using PokeApiDemo.Data;
using PokeApiDemo.Models;

namespace PokeApiDemo.Services
{
    public class PokemonService(HttpClient httpClient, IFileService fileService, ApplicationDbContext context) : IPokemonService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IFileService _fileService = fileService;
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Pokemon>> GetRandomPokemons(int count)
        {
            var pokemonList = new List<Pokemon>();
            var random = new Random();
            var tasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                var id = random.Next(1, 1008); // Ajustar com base na quantidade de Pokémon disponíveis
                tasks.Add(Task.Run(async () =>
                {
                    var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}/");
                    var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);
                    var spriteUrl = pokemon.Sprites.FrontDefault;
                    pokemon.SpriteBase64 = await _fileService.GetBase64SpriteAsync(spriteUrl);
                    pokemonList.Add(pokemon);
                }));
            }

            await Task.WhenAll(tasks);
            return pokemonList;
        }

        public async Task<Pokemon> GetPokemonById(int id)
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}/");
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);
            var spriteUrl = pokemon.Sprites.FrontDefault;
            pokemon.SpriteBase64 = await _fileService.GetBase64SpriteAsync(spriteUrl);
            return pokemon;
        }

        public async Task CapturePokemon(CapturedPokemon capturedPokemon)
        {
            _context.CapturedPokemons.Add(capturedPokemon);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CapturedPokemon>> GetCapturedPokemons()
        {
            return await Task.FromResult(_context.CapturedPokemons.ToList());
        }
    }
}
