using ExcelDataReader;
using System.Data;

namespace ContactManager.UI.Helper
{
    public static class ReadExcelToDataTable
    {
        public static DataSet ReadExcelFile(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // Assuming the first row contains column headers
                        }
                    });
                    return dataSet;
                }
            }
        }
    }
}
