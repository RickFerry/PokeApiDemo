using Microsoft.AspNetCore.Mvc;
using PokeApiDemo.Models;
using PokeApiDemo.Services;

namespace PokeApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController(IPokemonService pokemonService) : ControllerBase
    {
        private readonly IPokemonService _pokemonService = pokemonService;

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomPokemons()
        {
            var pokemons = await _pokemonService.GetRandomPokemons(10);
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemonById(int id)
        {
            var pokemon = await _pokemonService.GetPokemonById(id);
            return Ok(pokemon);
        }

        [HttpPost("capture")]
        public async Task<IActionResult> CapturePokemon([FromBody] CapturedPokemon capturedPokemon)
        {
            await _pokemonService.CapturePokemon(capturedPokemon);
            return Ok();
        }

        [HttpGet("captured")]
        public async Task<IActionResult> GetCapturedPokemons()
        {
            var capturedPokemons = await _pokemonService.GetCapturedPokemons();
            return Ok(capturedPokemons);
        }
    }
}
