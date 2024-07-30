namespace PokeApiDemo.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Sprite Sprites { get; set; }
        public required string SpriteBase64 { get; set; }
        public required Evolution[] Evolutions { get; set; }
    }

    public class Sprite
    {
        public required string FrontDefault { get; set; }
    }

    public class Evolution
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
