using CsvHelper;
using System.Globalization;
using TransactionManagement.Model.Consts;
using TransactionManagement.Model.CSVmodelDto;
using TransactionManagement.Model.ExceptionModel;
using TransactionManagement.Services.Interface;

namespace TransactionManagement.Services
{
    public class CSVService : ICSVService
    {
        public byte[] CreateCSV<T>(IEnumerable<T> data)
        {
            if (data is null)
            {
                throw new NotFoundException(ConstantError.GetNullForException());
            }

            try
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
            catch (Exception e)
            {
                throw new CsvCreateErrorException(ConstantError.CsvHandlerErrorForException(nameof(CreateCSV), e.Message));
            }
        }

        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            List<T> result = new List<T>();

            try
            {
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TransactRecordReadMap>();
                    var records = csv.GetRecords<T>();
                    foreach (var item in records)
                    {
                        result.Add(item);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                throw new CsvReadErrorException(ConstantError.CsvHandlerErrorForException(nameof(ReadCSV), e.Message));
            }
        }
    }
}
