using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IResourceService
    {
        Task<(byte[], string)> GetProfileImage(int id, string authToken);
    }
}
