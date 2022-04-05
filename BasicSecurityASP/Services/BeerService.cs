using System.Text.Json;
using BasicSecurityASP.Models;

namespace BasicSecurityASP.Services
{
    public class BeerService:IBeerService
    {
        private string path = @"C:\Users\mmartineza\Source\Repos\BasicSecurityASP\BasicSecurityASP\beers.json";
        public async Task<List<Beer>> Get()
        {
            string content = await File.ReadAllTextAsync(path);
            var beers = JsonSerializer.Deserialize<List<Beer>>(content);
            return beers;
        }
    }
}
