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
using System.Collections;
using System.Collections.Generic;
using Foms.Enums;
namespace Foms.Shared.Settings
{
    /// <summary>
    /// Octopus general parameters.
    /// </summary>
    [Serializable]
    public class ApplicationSettings
    {
        private static readonly IDictionary<string, ApplicationSettings> _generalsSettings = new Dictionary<string, ApplicationSettings>();

        public event ParamChangedEventHandler ParamChanged;

        private void _FillDefaultParamList()
        {
            _defaultParamList.Add(OGeneralSettings.BRANCHCODE, "KD");
            _defaultParamList.Add(OGeneralSettings.BRANCHNAME, "Not Set");
            _defaultParamList.Add(OGeneralSettings.BRANCHADDRESS, "Not Set");
            _defaultParamList.Add(OGeneralSettings.COUNTRY, "Not set");
            _defaultParamList.Add(OGeneralSettings.WEEKENDDAY1, 6);
            _defaultParamList.Add(OGeneralSettings.WEEKENDDAY2, 0);
            _defaultParamList.Add(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            _defaultParamList.Add(OGeneralSettings.CITYMANDATORY, true);
            _defaultParamList.Add(OGeneralSettings.GROUPMINMEMBERS, 2);
            _defaultParamList.Add(OGeneralSettings.CITYOPENVALUE, true);
            _defaultParamList.Add(OGeneralSettings.DISABLEFUTUREREPAYMENTS, true);
            _defaultParamList.Add(OGeneralSettings.ACCOUNTINGPROCESS, 1);
            _defaultParamList.Add(OGeneralSettings.LATEDAYSAFTERACCRUALCEASES, null);
            _defaultParamList.Add(OGeneralSettings.ALLOWSMULTIPLELOANS, 0);
            _defaultParamList.Add(OGeneralSettings.ALLOWSMULTIPLEGROUPS, 0);
            _defaultParamList.Add(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, 1);
            //_defaultParamList.Add(OGeneralSettings.SENTQUESTIONNAIRE, 0);
            _defaultParamList.Add(OGeneralSettings.NAME_FORMAT, "L U");
            _defaultParamList.Add(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            _defaultParamList.Add(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            _defaultParamList.Add(OGeneralSettings.CONTRACT_CODE_TEMPLATE, "BC/YY/PC-LC/ID");
            _defaultParamList.Add(OGeneralSettings.USEPROJECTS, 0);
            _defaultParamList.Add(OGeneralSettings.INTERESTS_ALSO_CREDITED_IN_FL, 0);
            _defaultParamList.Add(OGeneralSettings.ENFORCE_ID_PATTERN, 0);
            _defaultParamList.Add(OGeneralSettings.ID_WILD_CHAR_CHECK, 0);
            _defaultParamList.Add(OGeneralSettings.ID_PATTERN, "[A-Z]{2}[0-9]{7}");
            _defaultParamList.Add(OGeneralSettings.SAVINGS_CODE_TEMPLATE, "IC/BC/CS/ID");
            _defaultParamList.Add(OGeneralSettings.IMF_CODE, "IMF");
            _defaultParamList.Add(OGeneralSettings.MAX_NUMBER_INSTALLMENT, "MAX_NUMBER_INSTALLMENT");
            _defaultParamList.Add(OGeneralSettings.CHECK_BIC_CODE, 1);
            _defaultParamList.Add(OGeneralSettings.ACCOUNTING_EXPORT_MODE, "DEFAULT");
            _defaultParamList.Add(OGeneralSettings.PENDING_REPAYMENT_MODE, "NONE");
            _defaultParamList.Add(OGeneralSettings.PENDING_SAVINGS_MODE, "NONE");
            _defaultParamList.Add(OGeneralSettings.BAD_LOAN_DAYS, "180");
            _defaultParamList.Add(OGeneralSettings.VAT_RATE, 0);
            _defaultParamList.Add(OGeneralSettings.CEASE_LAIE_DAYS, 1000);
        }

        #region Internal stuff

        //private static ApplicationSettings _theUniqueInstance;
        private Hashtable _dbParamList;
        private readonly Hashtable _defaultParamList;
        
        public static void SuppressRemotingInfos(string pMd5)
        {
            if (_generalsSettings.ContainsKey(pMd5))
            {
                _generalsSettings.Remove(pMd5);
            }
        }


        private ApplicationSettings()
        {
            _dbParamList = new Hashtable();
            _defaultParamList = new Hashtable();
            _FillDefaultParamList();
        }


        public static ApplicationSettings GetInstance(string pMd5)
        {
            if (!_generalsSettings.ContainsKey(pMd5))
                _generalsSettings.Add(pMd5, new ApplicationSettings());
            return _generalsSettings[pMd5];
        }

        public void AddParameter(string key, object val)
        {
            if (val is bool)
                if (((bool)val)) _dbParamList.Add(key, "1"); else _dbParamList.Add(key, "0");

            else
                _dbParamList.Add(key, val);
        }

        private void FireParamChanged(ParamChangedEventArgs e)
        {
            if (ParamChanged != null)
            {
                ParamChanged(this, e);
            }
        }

        public void UpdateParameter(string pKey, object pValue)
        {
            if (pValue is bool)
                if (((bool)pValue)) _dbParamList[pKey] = "1"; else _dbParamList[pKey] = "0";

            else
                _dbParamList[pKey] = pValue;
            var args = new ParamChangedEventArgs(pKey, pValue);
            FireParamChanged(args);
        }

        public void DeleteAllParameters()
        {
            _dbParamList = new Hashtable();
        }

        public Hashtable DbParamList
        {
            get { return _dbParamList; }
        }

        public Hashtable DefaultParamList
        {
            get { return _defaultParamList; }
        }

        public object GetSpecificParameter(string name)
        {
            if (_dbParamList.ContainsKey(name))
                return _dbParamList[name];
            return null;
        }

        #endregion

        public string IDPattern
        {
            get
            {
                 return (string)GetSpecificParameter(OGeneralSettings.ID_PATTERN);
            }
        }
        public bool IsAllowMultipleLoans
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.ALLOWSMULTIPLELOANS).ToString() == "1";
            }
        }

        public bool IsAllowMultipleGroups
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.ALLOWSMULTIPLEGROUPS).ToString() == "1";
            }
        }

        public bool UseProjects
        {
            get { return GetSpecificParameter(OGeneralSettings.USEPROJECTS).ToString() == "1"; }
        }

        public bool IsCalculationLateFeesDuringHolidays
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS).ToString() == "1";
            }
        }

        public bool IsFutureRepaymentsDisabled
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.DISABLEFUTUREREPAYMENTS).ToString() == "1";
            }
        }
        public int? LateDaysAfterAccrualCeases
        {
            get {
                return GetSpecificParameter(OGeneralSettings.LATEDAYSAFTERACCRUALCEASES) == null
                           ? (int?) null
                           : Convert.ToInt32(GetSpecificParameter(OGeneralSettings.LATEDAYSAFTERACCRUALCEASES));
            }
        }

        /*public int? SentQuestionnaire
        {
            get {
                return GetSpecificParameter(OGeneralSettings.SENTQUESTIONNAIRE) == null
                           ? (int?) null
                           : Convert.ToInt32(GetSpecificParameter(OGeneralSettings.SENTQUESTIONNAIRE));
            }
        }*/
        
        public int GroupMinMembers
        {
            get { return Convert.ToInt32(GetSpecificParameter(OGeneralSettings.GROUPMINMEMBERS)); }
        }


        public OAccountingProcesses AccountingProcesses
        {
            get
            {
                return (OAccountingProcesses)Convert.ToInt32(GetSpecificParameter(OGeneralSettings.ACCOUNTINGPROCESS));
            }
        }

        public bool IsCityMandatory
        {
            get { return GetSpecificParameter(OGeneralSettings.CITYMANDATORY).ToString() == "1"; }
        }

        public bool PayFirstInterestRealValue
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE).ToString() == "1";
            }
        }

        public bool IsCityAnOpenValue
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.CITYOPENVALUE).ToString() == "1";
            }
        }

        public bool IsOlbBeforeRepayment
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.OLBBEFOREREPAYMENT).ToString() == "1";
            }
        }

        public string Country
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.COUNTRY).ToString();
            }
        }

        public string BranchCode
        {
            get
            {
                return (string)GetSpecificParameter(OGeneralSettings.BRANCHCODE);
            }
        }

        public string BranchName
        {
            get
            {
                return (string)GetSpecificParameter(OGeneralSettings.BRANCHNAME);
            }
        }

        public string AccountingExportMode
        {
            get { return (string)GetSpecificParameter(OGeneralSettings.ACCOUNTING_EXPORT_MODE); }
        }

        public int MaxNumberInstallment
        {
            get
            {
                return Convert.ToInt32(GetSpecificParameter(OGeneralSettings.MAX_NUMBER_INSTALLMENT));
            }
        }


        public string BranchAddress
        {
            get
            {
                return (string)GetSpecificParameter(OGeneralSettings.BRANCHADDRESS);
            }
        }

        /// <summary>
        /// 0 = sunday, 1 = monday...
        /// </summary>
        public int CeaseLateDays
        {
            get
            {
                return Convert.ToInt32(GetSpecificParameter(OGeneralSettings.CEASE_LAIE_DAYS));
            }
        }

        public int WeekEndDay1
        {
            get
            {
                return Convert.ToInt32(GetSpecificParameter(OGeneralSettings.WEEKENDDAY1));
            }
        }
        /// <summary>
        /// 0 = sunday, 1 = monday...
        /// </summary>
        public int WeekEndDay2
        {
            get
            {
                return Convert.ToInt32(GetSpecificParameter(OGeneralSettings.WEEKENDDAY2));
            }
        }

        public bool InterestsCreditedInFL
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.INTERESTS_ALSO_CREDITED_IN_FL).ToString() == "1";
            }
        }
        public bool IDWildCharCheck
        {
            get 
            {
                return GetSpecificParameter(OGeneralSettings.ID_WILD_CHAR_CHECK).ToString() == "1";
            }
        }
        public bool EnforceIDPattern
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.ENFORCE_ID_PATTERN).ToString() == "1";
            }
        }
        public bool DoNotSkipNonWorkingDays
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE).ToString() == "1";
            }
        }

        public bool IsIncrementalDuringDayOff
        {
            get
            {
                return GetSpecificParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF).ToString() == "1";
            }
        }

        public string FirstNameFormat
        {
            get
            {
                var format = (string) GetSpecificParameter(OGeneralSettings.NAME_FORMAT);
                var retval = "L";
                if (!string.IsNullOrEmpty(format))
                {
                    var items = format.Trim().Split(' ');
                    if (items.Length >= 1)
                    {
                        retval = items[0];
                    }
                }
                return retval;
            }
        }

        public string LastNameFormat
        {
            get
            {
                var format = (string) GetSpecificParameter(OGeneralSettings.NAME_FORMAT);
                var retval = "U";
                if (!string.IsNullOrEmpty(format))
                {
                    var items = format.Trim().Split();
                    if (2 == items.Length)
                    {
                        retval = items[1];
                    }
                }
                return retval;
            }
        }

        public string ContractCodeTemplate
        {
            get { return (string)GetSpecificParameter(OGeneralSettings.CONTRACT_CODE_TEMPLATE); }
        }

        public string SavingsCodeTemplate
        {
            get { return (string)GetSpecificParameter(OGeneralSettings.SAVINGS_CODE_TEMPLATE); }
        }

        public string ImfCode
        {
            get { return (string)GetSpecificParameter(OGeneralSettings.IMF_CODE); }
        }

        public bool CheckBicCode
        {
            get { return GetSpecificParameter(OGeneralSettings.CHECK_BIC_CODE).ToString() == "1"; }
        }

        public string PendingRepaymentMode
        {
            get { return GetSpecificParameter(OGeneralSettings.PENDING_REPAYMENT_MODE).ToString(); }
        }

        public string PendingSavingsMode
        {
            get { return GetSpecificParameter(OGeneralSettings.PENDING_SAVINGS_MODE).ToString(); }
        }

        public int BadLoanDays
        {
            get { return Convert.ToInt32(GetSpecificParameter(OGeneralSettings.BAD_LOAN_DAYS).ToString()); }
        }

        public int VatRate
        {
            get
            {
                object value = GetSpecificParameter(OGeneralSettings.VAT_RATE);
                return Convert.ToInt32(value);
            }
        }
    }
}
