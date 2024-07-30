namespace PokeApiDemo.Models
{
    public class CapturedPokemon
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string MasterName { get; set; }
    }
}
