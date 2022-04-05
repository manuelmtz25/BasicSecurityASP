using BasicSecurityASP.Models;

namespace BasicSecurityASP.Services
{
    public interface IBeerService
    {
        public Task<List<Beer>> Get();
    }
}
