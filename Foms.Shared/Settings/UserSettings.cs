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
using System.Reflection;

namespace Foms.Shared.Settings
{
	/// <summary>
	/// Application settings.
	/// </summary>
	[Serializable]
	public static class UserSettings
    {
	    public static string GetReportPath
	    {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Reports"); } 
	    }

        public static string GetTemplatePath
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Template"); }
        }

	    public static string GetInternalReportPath
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Reports\Internal"); }
        }

	    public static string GetUpdatePath
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Update"); }
        }

	    public static string Language
        {
            get { return UserSettingsStore.Default.LANGUAGE; }
            set { _SetSetting("LANGUAGE", value); }
        }

        public static string BackupPath
        {
            get
            {
                if (string.IsNullOrEmpty(UserSettingsStore.Default.BACKUP_PATH))
                {
                    if (Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Backup")))
                    {
                        _SetSetting("BACKUP_PATH", Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Backup"));
                    }
                    else
                    {
                        _SetSetting("BACKUP_PATH", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                    }
                }
                return UserSettingsStore.Default.BACKUP_PATH;
            }
            set { _SetSetting("BACKUP_PATH", value); }
        }

        public static string ExportConsoPath
        {
            get
            {
                if (string.IsNullOrEmpty(UserSettingsStore.Default.EXPORT_PATH))
                {
                    _SetSetting("EXPORT_PATH", Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Export"));
                }
                return UserSettingsStore.Default.EXPORT_PATH;
            }
            set { _SetSetting("EXPORT_PATH", value); }
        }

	    public static bool AutoUpdate
	    {
            get { return UserSettingsStore.Default.AUTO_UPDATE; }
            set { _SetSetting("AUTO_UPDATE", value); }
	    }

        private static void _SetSetting(string pPropertyName, string pValue)
        {
            UserSettingsStore.Default[pPropertyName] = pValue;
            UserSettingsStore.Default.Save();
        }

        private static void _SetSetting(string pPropertyName, bool pValue)
        {
            UserSettingsStore.Default[pPropertyName] = pValue;
            UserSettingsStore.Default.Save();
        }
	}
}

