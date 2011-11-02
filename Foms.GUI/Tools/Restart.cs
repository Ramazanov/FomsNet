//Octopus MFS is an integrated suite for managing a Micro Finance Institution
// -> clients, contracts, accounting, reporting and risk
//Copyright 2007,2007 OCTO Technology
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
// Contacts: business@octopusnetwork.org 
//           tech@octopusnetwork.org 

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Foms.Shared.Settings;

namespace Foms.GUI.Tools
{
    /// <summary>
    /// Application restart.<br/>
    /// </summary>
    public static class Restart
    {
        private const string RESTARTER = "Octopus.GUI.Restarter.exe";
        private const string ONLINE = " -online";

        /// <summary>
        /// Launches the application restarter.<br/>
        /// </summary>
        public static void LaunchRestarter()
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            path = Path.Combine(path, RESTARTER);
            
            Process.Start(path);

            Application.Exit();
        }
    }
}
