using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_XLS
{
    public static class ExcelDataReader
    {
        // See https://github.com/ExcelDataReader/ExcelDataReader

        public static void ReadData(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                //CreateCsvReader
                IDataReader reader;
                //if (filePath.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase) ||
                //    filePath.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
                //    reader = ExcelReaderFactory.CreateCsvReader(stream);
                //else
                //    reader = ExcelReaderFactory.CreateReader(stream);

                try
                {
                    reader = ExcelReaderFactory.CreateReader(stream);
                }
                catch
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream);
                }

                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        var sheetName = (reader as IExcelDataReader)?.Name;

                        while (reader.Read()) // next record
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                var v = reader.GetValue(i);
                                if (double.TryParse((string)v, out _))
                                {
                                    var d = reader.GetDouble(i);
                                }
                                // reader.GetDouble(0);
                                v = null;
                            }
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    //var result = reader.AsDataSet();

                    // The result of each spreadsheet is in result.Tables

                    //return result;
                }

                reader.Close();
            }
        }
    }
}
