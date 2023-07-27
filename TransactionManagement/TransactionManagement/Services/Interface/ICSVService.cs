using TransactionManagement.Model;

namespace TransactionManagement.Services.Interface
{
    public interface ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file);
        public byte[] CreateCSV<T>(IEnumerable<T> data);
    }
}
