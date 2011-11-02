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
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Foms.Extensions;
using Microsoft.Win32;

namespace Foms.Shared.Settings
{
    /// <summary>
    /// Technical parameters.
    /// </summary
    [Serializable]
    [Export(typeof(ITechnicalSettings))]
    public static class TechnicalSettings
    {
        public const string ASSEMBLY_VERSION = "1.1.1.*";
        public const string COMPANY = "Octopus Micro Finance";
        public const string PRODUCT = "OCTOPUS Micro Finance Suite";
        public const string COPYRIGHT = "Octopus Micro Finance";
        public const string TRADEMARK = "Octopus Micro Finance";

        private static readonly string REGISTRY_PATH = "Software\\Octo technology\\Octopus " + TechnicalSettingsStore.Default.VERSION;
        private static readonly string REGISTRY_PATH_REMOTING_CLIENT = @"Software\Octopus MicroFinance\OMFS Remoting Client\" + TechnicalSettingsStore.Default.VERSION;

        private static bool _useRegistry;
        
        public static bool CheckSettings()
        {

            if (Registry.LocalMachine.OpenSubKey(REGISTRY_PATH) != null)
            {
                _useRegistry = true;
                _GetSettingsFromRegistry();
            }

            return _CheckSettings();
        }

        private static bool _CheckSettings()
        {
            foreach (string key in new List<string> { "DATABASE_NAME", 
                                                      "DATABASE_PASSWORD", 
                                                      "DATABASE_SERVER_NAME",
                                                      "DATABASE_LOGIN_NAME",
                                                      "DATABASE_TIMEOUT", 
                                                      "VERSION", 
                                                      "COMPANY", 
                                                      "COPYRIGHT", 
                                                      "TRADEMARK", 
                                                      "PRODUCT", 
                                                      "SENT_QUESTIONNAIRE" })
            {
                if (string.IsNullOrEmpty(TechnicalSettingsStore.Default[key].ToString())) 
                    return false;
            }

            return true;
        }

        public static string Copyright
        {
            get { return TechnicalSettingsStore.Default.COPYRIGHT; }
        }

        public static string Product
        {
            get { return TechnicalSettingsStore.Default.PRODUCT; }
        }

        public static string Company
        {
            get { return TechnicalSettingsStore.Default.COMPANY; }
        }
      
        public static string Trademark
        {
            get { return TechnicalSettingsStore.Default.TRADEMARK; }
        }


        public static bool UseDebugMode
        {
            get { return TechnicalSettingsStore.Default.DEBUG_MODE; }
            set { _SetSetting("DEBUG_MODE", value); }
        }

        public static int DebugLevel
        {
            get { return TechnicalSettingsStore.Default.DEBUG_LEVEL; }
            set { _SetSetting("DEBUG_LEVEL", value); }
        }

        public static string DatabaseServerName
        {
            get { return TechnicalSettingsStore.Default.DATABASE_SERVER_NAME; }
            set { _SetSetting("DATABASE_SERVER_NAME", value); }
        }

        public static string DatabaseName
        {
            get { return TechnicalSettingsStore.Default.DATABASE_NAME; }
            set { _SetSetting("DATABASE_NAME", value); }
        }

        public static string DatabasePassword
        {
            get { return TechnicalSettingsStore.Default.DATABASE_PASSWORD; }
            set { _SetSetting("DATABASE_PASSWORD", value); }
        }

        public static string DatabaseLoginName
        {
            get { return TechnicalSettingsStore.Default.DATABASE_LOGIN_NAME; }
            set { _SetSetting("DATABASE_LOGIN_NAME", value); }
        }

        public static int DatabaseTimeout
        {
            get { return Convert.ToInt32(TechnicalSettingsStore.Default.DATABASE_TIMEOUT); }
            set { _SetSetting("DATABASE_TIMEOUT", value.ToString()); }
        }


        public static bool SentQuestionnaire
        {
            get { return TechnicalSettingsStore.Default.SENT_QUESTIONNAIRE; }
            set { _SetSetting("SENT_QUESTIONNAIRE", value); }
        }

        public static string SoftwareVersionWithBuild
        {
            get { return string.Format("v{0}.{1}", TechnicalSettingsStore.Default.VERSION, SoftwareBuild); }
        }

        public static string SoftwareVersion
        {
            get { return string.Format("v{0}",TechnicalSettingsStore.Default.VERSION); }
        }

        public static string SoftwareBuild
        {
            get { return GetRevisionNumberFromFile(); }
        }

        private static string GetRevisionNumberFromFile()
        {
            string revisionNumber;

            try
            {
                TextReader textReader = new StreamReader(Application.StartupPath + "\\" + "BuildLabel.txt");
                revisionNumber = textReader.ReadLine();
                if (string.IsNullOrEmpty(revisionNumber))
                    return string.Empty;
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }

            try
            {
                Convert.ToInt32(revisionNumber);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return revisionNumber;
        }

        public static bool IsThisVersionNewer(string pVersion)
        {
            try
            {
                Version v = new Version(pVersion.ToLower().Replace("v", ""));
                Version c = new Version(TechnicalSettingsStore.Default.VERSION.ToLower());
                return (v > c);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        private static void _GetSettingsFromRegistry()
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(REGISTRY_PATH);
            if (null == regkey) 
                return;
            foreach (string key in new List<string> { "DATABASE_NAME", 
                                                      "DATABASE_LOGIN_NAME", 
                                                      "DATABASE_PASSWORD", 
                                                      "DATABASE_SERVER_NAME", 
                                                      "DATABASE_TIMEOUT", 
                                                      "BRANCH_ID",
                                                      "SENT_QUESTIONNAIRE" })
            {
                object o = regkey.GetValue(key);

                if (key != "SENT_QUESTIONNAIRE")
                {
                    TechnicalSettingsStore.Default[key] = o == null ? string.Empty : o.ToString();
                }
                else
                    TechnicalSettingsStore.Default[key] = o == null ? false : Boolean.Parse(o.ToString());
            }
            regkey.Close();
        }

        private static  void GetRemotingClientSettingsFromRegistry()
        {
            foreach (string key in new List<string> { "REMOTING_SERVER", "REMOTING_SERVER_PORT" })
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(REGISTRY_PATH_REMOTING_CLIENT);
                if (registryKey != null)
                {
                    var value = registryKey.GetValue(key);

                    if (TechnicalSettingsStore.Default[key] is string)
                        TechnicalSettingsStore.Default[key] = value == null ? string.Empty : value.ToString();
                    else
                        TechnicalSettingsStore.Default[key] = value == null ? 0 : Convert.ToInt32(value);
                }
            }
        }

        private static void _SetSetting(string pPropertyName, string pValue)
        {
            TechnicalSettingsStore.Default[pPropertyName] = pValue;
            TechnicalSettingsStore.Default.Save();

            if (pPropertyName == "REMOTING_SERVER")
                _SetSettingsInRegistry(REGISTRY_PATH_REMOTING_CLIENT, pPropertyName, pValue);

            else
            {
                if (!_useRegistry) return;

                _SetSettingsInRegistry(REGISTRY_PATH, pPropertyName, pValue);
            }
        }

        private static void _SetSetting(string pPropertyName, bool pValue)
        {
            TechnicalSettingsStore.Default[pPropertyName] = pValue;
            TechnicalSettingsStore.Default.Save();

            if (pPropertyName == "SENT_QUESTIONNAIRE")
                _SetSettingsInRegistry(REGISTRY_PATH, pPropertyName, pValue.ToString());
        }

        private static void _SetSetting(string pPropertyName, int pValue)
        {
            TechnicalSettingsStore.Default[pPropertyName] = pValue;
            TechnicalSettingsStore.Default.Save();

            if (pPropertyName == "REMOTING_SERVER_PORT")
                _SetSettingsInRegistry(REGISTRY_PATH_REMOTING_CLIENT, pPropertyName, pValue.ToString());
        }

        private static void _SetSettingsInRegistry(string pRegistryPath, string pPropertyName, string pValue)
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(pRegistryPath, true);
            if (reg == null) 
                return;

            reg.SetValue(pPropertyName, pValue);
            reg.Close();
        }       


        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder connectionStringBldr = new SqlConnectionStringBuilder();

                connectionStringBldr.DataSource = DatabaseServerName;
                connectionStringBldr.InitialCatalog = DatabaseName;
                //connectionStringBldr.IntegratedSecurity = 
                connectionStringBldr.UserID = DatabaseLoginName;
                connectionStringBldr.Password = DatabasePassword;
                connectionStringBldr.ConnectTimeout = DatabaseTimeout;
                
                return connectionStringBldr.ConnectionString;
            }
        }
    }
}