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

namespace Foms.CoreDomain
{
    [Serializable]
    public class MenuObject : IEquatable<MenuObject>
    {
        private int id;
        private string name;
        private bool notSavedInDBYet;

        public MenuObject()
        {
            notSavedInDBYet = false;
        }
        public MenuObject(int pID, string pName, bool pNotSavedYet)
        {
            id = pID;
            name = pName;
            notSavedInDBYet = pNotSavedYet;
        }
        public bool NotSavedInDBYet
        {
            get { return notSavedInDBYet; }
            set { notSavedInDBYet = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public override string ToString()
        {
            return Name;
        }
        public bool Equals(MenuObject b)
        {
            return string.Equals(name, b.name);
        }

        public override bool Equals(object obj)
        {
            MenuObject a = obj as MenuObject;
            return a != null && Equals(a);
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(name) != null ? name.GetHashCode() : base.GetHashCode();
        }
        public static bool operator ==(MenuObject a, Object b)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            if (b is string)
            {
                string str = b.ToString();
                return str.ToUpper().Equals(a.name.ToUpper());
            }

            MenuObject tryToConvert = (MenuObject)b;
            if (tryToConvert == null) return false;

            return Convert.ToBoolean(string.Compare(tryToConvert.name, a.name, true));
        }
        public static bool operator !=(MenuObject a, System.Object b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return false;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return true;
            }
            if (b is string)
            {
                return !b.Equals(a.name);
            }

            MenuObject tryToConvert = (MenuObject) b;
            if (tryToConvert == null) return true;

            return !a.name.ToUpper().Equals(tryToConvert.name.ToUpper());
        }
    }
}
