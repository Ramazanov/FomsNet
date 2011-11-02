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
using ICSharpCode.SharpZipLib.Zip;

namespace Foms.Shared
{
    /// <summary>
    /// Summary description for ZipHelper.
    /// </summary>
    [Serializable]
    public class ZipHelper
    {
        private string StrPathFile; // stock path+file

        private ZipHelper() {}

        public ZipHelper(string pStrFiles)
            {
                StrPathFile = pStrFiles;
            }

        public bool Compress(string pStrExtension)
        {
            bool CompressDone = false;
            try
            {
                int IntSize = 4096;
                byte[] data = new byte[4096];
                string StrPathZip = Path.GetDirectoryName(StrPathFile) + "\\" + (Path.GetFileNameWithoutExtension(StrPathFile)) + pStrExtension;
                ZipOutputStream MyZipOutputStream = new ZipOutputStream(File.Create(StrPathZip));    // création du fichier zip vide
                MyZipOutputStream.SetLevel(9); // Niveau de compression de 0 (faible) à 9 (fort)
                FileStream fs = File.OpenRead(StrPathFile); // lecture du fichier non compréssé
                ZipEntry theEntry = new ZipEntry(Path.GetFileName(StrPathFile));  // prends le nom que du fichier en creant le zip
                MyZipOutputStream.PutNextEntry(theEntry);      // on selectionne le fichier à l'interieur du zip
                while (IntSize > 0)                // on lit le fichier jusqu'à la fin par bloc de 4096 bytes
                {
                    IntSize = fs.Read(data, 0, IntSize);
                    if (IntSize > 0)
                    {
                        MyZipOutputStream.Write(data, 0, IntSize);
                    }
                }
                MyZipOutputStream.Finish(); // on ferme le flux zip
                MyZipOutputStream.Close();  // on ferme le flux zip
                fs.Close();                 // on ferme le flux d'écriture
                CompressDone = true; // on met la variable sur réussi
                return CompressDone;
            }
            catch
            {
                return CompressDone;
            }
        }

        public static void SaveFileToZip(string pFileToZip, ZipOutputStream s)
        {
            ZipEntry entry = new ZipEntry(Path.GetFileName(pFileToZip));
            s.PutNextEntry(entry);
            using ( FileStream fs = File.OpenRead(pFileToZip) )
            {
                byte[] buffer = new byte[4096];
                // Using a fixed size buffer here makes no noticeable difference for output
                // but keeps a lid on memory usage.
                int sourceBytes;
                do 
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                    s.Write(buffer, 0, sourceBytes);
                } while ( sourceBytes > 0 );
            }
        }

        /// <summary>
        /// Zip the file.
        /// </summary>
        /// <param name="pFileToZip">File to zip full path</param>
        /// <param name="pZippedFile">Zipped file full path (should be a *.zip file name, but not mandatory)</param>
        public static void ZipFile(string pFileToZip, string pZippedFile)
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(pZippedFile)))
            {
                s.SetLevel(9); // Best compression
                SaveFileToZip(pFileToZip,s);
            }
        }

        /// <summary>
        /// Unzip the first file found in the given ZIP.
        /// </summary>
        /// <param name="pZipFileToUnzip">Zip file to unzip</param>
        /// <returns>Unzipped file</returns>
        public static string UnzipFile(string pZipFileToUnzip)
        {
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(pZipFileToUnzip)))
            {
                ZipEntry entry = s.GetNextEntry();	
                if ((entry != null) && (entry.Name.Length > 0))
                {
                    string unzippedFile = Path.Combine(Path.GetDirectoryName(pZipFileToUnzip), entry.Name);
                    UnzipEntry(s, unzippedFile);
                    return unzippedFile;
                }
            }
            return null;
        }

       

        public static void UnzipEntry(ZipInputStream s, string unzippedFile)
        {
            using (FileStream streamWriter = File.Create(unzippedFile)) 
            {				
                int size = 0;
                byte[] data = new byte[2048];
                while (true) 
                {
                    size = s.Read(data, 0, data.Length);
                    if (size > 0) 
                    {
                        streamWriter.Write(data, 0, size);
                    } 
                    else 
                    {
                        break;
                    }
                }
            }
        }
    }
}
