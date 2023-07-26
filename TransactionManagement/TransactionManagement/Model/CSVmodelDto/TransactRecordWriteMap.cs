using CsvHelper.Configuration;
using System.Globalization;

namespace TransactionManagement.Model.CSVmodelDto
{
    public class TransactRecordWriteMap : ClassMap<TransactRecord>
    {
        public TransactRecordWriteMap()
        {
            Map(m => m.TransactionId);
            Map(m => m.Status);
            Map(m => m.Type);
            Map(m => m.ClientName);

            Map(m => m.Amount).Name("Amount").Convert(x =>
            {
                double amount = x.Value.Amount;
                return amount.ToString("C", CultureInfo.GetCultureInfo("en-US"));
            });

        }
    }
}
