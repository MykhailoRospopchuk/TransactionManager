using CsvHelper;
using System.Globalization;
using TransactionManagement.Model.CSVmodelDto;

namespace TransactionManagement.Services
{
    public class CSVService : ICSVService
    {
        public byte[] CreateCSV<T>(IEnumerable<T> data)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.Context.RegisterClassMap<TransactRecordWriteMap>();
                    csvWriter.WriteRecords(data);
                }
            }

            return memoryStream.ToArray();
        }

        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            IEnumerable<T> records;

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TransactRecordReadMap>();
                records = csv.GetRecords<T>();
                foreach (var item in records)
                {
                    yield return item;
                }
            }
        }
    }
}
