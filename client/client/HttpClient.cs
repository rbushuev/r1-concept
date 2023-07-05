using client.Models;
using System.Net.Http.Json;

namespace client
{
    public static class HttpHelper
    {
        private static readonly HttpClient _httpClient = new();

        public static async Task DownloadFileAsync(string uri, string outputPath)
        {
            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            File.WriteAllBytes(outputPath, fileBytes);
        }
    }
}
