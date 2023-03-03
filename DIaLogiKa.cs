using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIaLOGIKa.b2xtranslator.Spreadsheet.XlsFileFormat;
using DIaLOGIKa.b2xtranslator.SpreadsheetMLMapping;
using DIaLOGIKa.b2xtranslator.StructuredStorage.Reader;
using DIaLOGIKa.b2xtranslator.OpenXmlLib.SpreadsheetML;
using System.IO;

namespace CSharp_XLS
{
    public static class DIaLogiKa
    {
        public static void xls2xlsx(string file, string filex)
        {
            File.Delete(filex);
            using (StructuredStorageReader reader = new StructuredStorageReader(file))
            {
                var xlsDoc = new XlsDocument(reader);

                var outType = Converter.DetectOutputType(xlsDoc);
                var conformOutputFile = Converter.GetConformFilename(filex, outType);
                using (var spreadx = SpreadsheetDocument.Create(conformOutputFile, outType))
                {
                    Converter.Convert(xlsDoc, spreadx);
                }
            }
        }
    }
}
