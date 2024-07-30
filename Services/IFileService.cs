namespace PokeApiDemo.Services
{
    public interface IFileService
    {
        Task<string> GetBase64SpriteAsync(string url);
    }
}
