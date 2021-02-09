using System.Threading.Tasks;

namespace LocalLibrary.Core.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string username);
    }
}