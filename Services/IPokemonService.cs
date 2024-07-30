using PokeApiDemo.Models;

namespace PokeApiDemo.Services
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetRandomPokemons(int count);
        Task<Pokemon> GetPokemonById(int id);
        Task CapturePokemon(CapturedPokemon capturedPokemon);
        Task<IEnumerable<CapturedPokemon>> GetCapturedPokemons();
    }
}
