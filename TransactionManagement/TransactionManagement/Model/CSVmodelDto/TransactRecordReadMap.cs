using CsvHelper.Configuration;
using System.Globalization;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.Enums;

namespace TransactionManagement.Model.CSVmodelDto
{
    public class TransactRecordReadMap : ClassMap<TransactRecord>
    {
        public TransactRecordReadMap()
        {
            Map(m => m.TransactionId);

            //In case need convert
            //Map(m => m.Status).Index(0).Convert(row => Enum.Parse<StatusTransaction>(row.Row.GetField<string>("Status")));
            Map(m => m.Status);

            Map(m => m.Type);
            Map(m => m.ClientName);

            Map(m => m.Amount).Name("Amount").Convert(x =>
            {
                string temp = x.Row.GetField<string>("Amount").Remove(0, 1);
                return Convert.ToDouble(temp, CultureInfo.GetCultureInfo("en-US"));
            });

        }
    }
}
