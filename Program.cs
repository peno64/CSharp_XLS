using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharp_XLS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DIaLogiKa.xls2xlsx(@"C:\brol\JobFiles\AAA Demo Reports\EAB_MultiER_105.xls", @"C:\brol\JobFiles\AAA Demo Reports\EAB_MultiER_105.xlsx");
            //Spire.xls2xlsx(@"C:\git\CSHARP\ConvertDataPath2Wex\Files\Example1\EAB_MultiER_105.xls", @"C:\git\CSHARP\ConvertDataPath2Wex\Files\Example1\EAB_MultiER_105.xlsx");

            var dataSet = ExcelDataReader.ReadData(@"C:\git\CSHARP\ConvertDataPath2Wex\Files\Example1\EAB_MultiER_105.xls");
        }
    }
}
