using System.Globalization;
using CsvHelper;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VehicleRunsheetMBAProj.Data.Repositories;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Utilities
{
    public class CsvRunsheetWriter
    {
        private readonly IUnitOfWork _unit;

        public CsvRunsheetWriter(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task WriteRunsheetToCsv(Runsheet runsheet)
        {
            await using var textWriter = new StreamWriter(@"C:\Users\alex_\source\repos\VehicleRunsheetMBA\VehicleRunsheetMBA\wwwroot\CsvFiles\OutputCsv.csv");

            await using var writer = new CsvWriter(textWriter, CultureInfo.InvariantCulture);
            writer.Configuration.Delimiter = ",";

            var result = await _unit.Runsheets.GetAllWithChildren();
            var last = result.FirstOrDefault();

            await writer.WriteRecordsAsync(runsheet.Trips);
        }
    }
}
