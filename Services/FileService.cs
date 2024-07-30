namespace PokeApiDemo.Services
{
    public class FileService(HttpClient httpClient) : IFileService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<string> GetBase64SpriteAsync(string url)
        {
            try
            {
                var imageBytes = await _httpClient.GetByteArrayAsync(url);
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as necessary
                throw new Exception($"An error occurred while fetching the sprite: {ex.Message}");
            }
        }
    }
}
