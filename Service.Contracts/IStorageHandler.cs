using System.IO;

namespace Service.Contracts
{
    public interface IStorageHandler
    {
        string StoreFile(Stream file);
        Stream RetrieveFile(string key);
    }
}
