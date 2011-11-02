//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace Foms.Shared
{
    /// <summary>
    /// Description résumée de ExportFile.
    /// </summary>
    [Serializable]
    public static class ExportFile
    {
        public static void SaveToFile(DataSet dataset, string fileName, string separator)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = _SaveTextToNewPath();
            if (fileName != null)
                _SaveTextToPath(fileName, dataset, separator);
        }

        public static string _SaveTextToNewPath()
        {
            string path = null;
            SaveFileDialog dlg = new SaveFileDialog
                                     {
                                         DefaultExt = "csv",
                                         Filter =
                                             "Plain Text (*.txt)|*.txt|Word Document (*.doc)|*.doc|CSV Document (*.csv)|*.csv|Excel Document (*.xls)|*.xls|All files (*.*)|*.*",
                                         FilterIndex = 1,
                                         InitialDirectory = Application.CommonAppDataPath
                                     };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                path = dlg.FileName;
            }

            return path;
        }

        private static void _SaveTextToPath(string path, DataSet dataSet, string separator)
        {
            StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.Unicode);

            foreach (DataRow curRow in dataSet.Tables[0].Rows)
            {
                string[] arr = new String[curRow.Table.Columns.Count];
                for (int i = 0; i < curRow.Table.Columns.Count; i++)
                    arr[i] = curRow.ItemArray[i].ToString();

                writer.WriteLine(string.Join(separator, arr));
            }
            writer.Close();
        }
    }
}