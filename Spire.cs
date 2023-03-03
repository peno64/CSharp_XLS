using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Xls;

namespace CSharp_XLS
{
    public static class Spire
    {
        public static void xls2xlsx(string file, string filex)
        {
            Workbook workbook = new Workbook();

            workbook.LoadFromFile(file);
            workbook.SaveToFile(filex, ExcelVersion.Version2016);
        }
    }
}
