﻿//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
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
using System.Collections.Generic;
using Foms.CoreDomain.EconomicActivities;

namespace Foms.CoreDomain.Clients
{
    [Serializable]
    public class Person : Client
    {
        public static char SEXE_MALE = 'M';
        public static char SEXE_FEMALE = 'F';
        private List<PersonCustomizableField> _fields;
        private string _firstName;
        private string _lastName;
        private char sex = SEXE_MALE;
        private int? livestockNumber;
        private string livestockType;
        private double? landplotSize;
        private double? homeSize;
        private int? homeTimeLivingIn;
        private OCurrency monthlyIncome;
        private OCurrency monthlyExpenditure;

        public DateTime? FirstContact { get; set; }
        public DateTime? FirstAppointment { get; set; }
        public string ProfessionalExperience { get; set; }
        public string ProfessionalSituation { get; set; }
        public bool Handicapped { get; set; }
        public string StudyLevel { get; set; }
        public string SSNumber { get; set; }
        public string CAFNumber { get; set; }
        public string HousingSituation { get; set; }
        public int? UnemploymentMonths { get; set; }
        public string BankSituation { get; set; }
        public string FamilySituation { get; set; }
        public string BirthPlace { get; set; }
        public string Nationality { get; set; }
        public string Image { get; set; }

        public string ImagePath { get; set; }
        public string Image2Path { get; set;}
        public bool IsImageAdded { get; set; }
        public bool IsImage2Added {get; set;}
        public bool IsImageDeleted { get; set; }
        public bool IsImage2Deleted { get; set; }
        public bool IsImageUpdated { get; set; }
        public bool IsImage2Updated { get; set; }

        public string IdentificationData { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool HouseHoldHead { get; set; }
        public int? NbOfDependents { get; set; }
        public int? NbOfChildren { get; set; }
        public int? ChildrenBasicEducation { get; set; }
        public string CapitalOthersEquipments { get; set; }
        public EconomicActivity Activity { get; set; }
        public int? Experience { get; set; }
        public int? NbOfPeople { get; set; }
        public string Comments { get; set; }

        public Bank PersonalBank { get; set; }
        public Bank BusinessBank { get; set; }
        public User FavouriteLoanOfficer { get; set; }
        public int? FavouriteLoanOfficerId { get; set; }

        public PovertyLevelIndicators PovertyLevelIndicators { get; set; }

        public Person() : base(new List<Project>(), OClientTypes.Person)
        {
            PovertyLevelIndicators = new PovertyLevelIndicators();
            _fields = new List<PersonCustomizableField>();
        }

        public override string Name
        {
            get { return string.Format("{0} {1}", _firstName, _lastName); }
            set { _firstName = value; }
        }

        public override IClient Copy()
        {
            Person clonedPerson = (Person)MemberwiseClone();
            return clonedPerson;
        }

        public void AddCustomizedField(PersonCustomizableField pField)
        {
            bool newField = true;
            for (int i = 0; i < _fields.Count; i++)
            {
                if (_fields[i].Number == pField.Number)
                {
                    string value = _fields[i].Value;
                    _fields[i] = pField;
                    _fields[i].Value = value;
                    newField = false;
                    break;
                }
            }

            if (newField)
                _fields.Add(pField);
        }

        public List<PersonCustomizableField> CustomizableFields
        {
            get { return _fields; }
            set { _fields = value; }
        }


        public char Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public int? LivestockNumber
        {
            get { return livestockNumber; }
            set { livestockNumber = value; }
        }

        public string LivestockType
        {
            get { return livestockType; }
            set { livestockType = value; }
        }

        public double? LandplotSize
        {
            get { return landplotSize; }
            set { landplotSize = value; }
        }

        public double? HomeSize
        {
            get { return homeSize; }
            set { homeSize = value; }
        }

        public int? HomeTimeLivingIn
        {
            get { return homeTimeLivingIn; }
            set { homeTimeLivingIn = value; }
        }

        public OCurrency MonthlyIncome
        {
            get { return monthlyIncome; }
            set { monthlyIncome = value; }
        }

        public OCurrency MonthlyExpenditure
        {
            get { return monthlyExpenditure; }
            set { monthlyExpenditure = value; }
        }

        public OCurrency MonthlyDisposable
        {
            get
            {
                if (monthlyIncome.HasValue && monthlyExpenditure.HasValue)
                {
                    return monthlyIncome.Value != -1 && monthlyExpenditure.Value != -1
                               ? (OCurrency)(monthlyIncome.Value - monthlyExpenditure.Value)
                               : null;
                }
                return null;
            }
        }

        public bool HasHome()
        {
            return homeSize.HasValue || homeTimeLivingIn.HasValue;
        }

        public bool HasLandPlot()
        {
            return landplotSize.HasValue;
        }

        public bool HasLivestock()
        {
            return livestockNumber.HasValue || livestockType != null;
        }

        public bool HasOtherOrganization()
        {
            return OtherOrgName != null || OtherOrgDebts.HasValue || OtherOrgAmount.HasValue;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}