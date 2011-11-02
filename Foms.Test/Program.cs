
using System;
using System.Diagnostics;
using Foms.Test.DataSet1TableAdapters;

namespace Foms.Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
         //   ConnectionManager connection = ConnectionManager.GetInstance();
            
            //Console.WriteLine(Foms.DatabaseConnection.ConnectionManager.CheckSQLDatabaseConnection());
            DataSet1 ds = new DataSet1();
            DataSet1TableAdapters.d999TableAdapter mz = new d999TableAdapter();
            mz.Fill(ds.d999);

            Console.WriteLine(ds.d999.Count);

            try
            {
         

                 Stopwatch s = new Stopwatch();
                s.Start();
 
                SQLXMLBULKLOADLib.SQLXMLBulkLoad4Class objBL = new SQLXMLBULKLOADLib.SQLXMLBulkLoad4Class();
                objBL.ConnectionString = "Provider=sqloledb;server=server7;database=TempA;uid=nariman;pwd=letsgo";
                objBL.ErrorLogFile = @"D:\temp\error.xml";
                objBL.KeepIdentity = true;
                objBL.CheckConstraints = true;
                //objBL.BulkLoad = true;
                //objBL.ForceTableLock = true;
                objBL.FireTriggers = true;
                objBL.Execute(@"D:\temp\schema.xml", @"D:\temp\data.xml");
               
                Console.WriteLine("{0} ms", s.ElapsedMilliseconds);
                 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();



        }
    }

}
