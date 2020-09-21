using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Managers
{
    public interface IFileEntityManager
    {
        Task<byte[]> Read(string fileEntityId, string fileEntityExtension);
        Task Save(string fileEntityId, string fileEntityExtension, byte[] bytes);
        Task Delete(string fileEntityId, string fileEntityExtension);
    }
}
