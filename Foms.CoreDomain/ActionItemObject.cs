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
    public class ActionItemObject : IEquatable<ActionItemObject>
    {
        private int id;
        private string _className;
        private string _methodName;
        private bool notSavedInDBYet;

        public ActionItemObject()
        {
            notSavedInDBYet = false;
        }
        public ActionItemObject(string pClassName, string pMethodName)
        {
            notSavedInDBYet = false;
            _className = pClassName;
            _methodName = pMethodName;
        }
        public ActionItemObject(int pID, string pClassName, string pMethodName, bool pNotSavedYet)
        {
            id = pID;
            _className = pClassName;
            _methodName = pMethodName;
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

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }
        public override string ToString()
        {
            return _className+_methodName;
        }
        public bool Equals(ActionItemObject b)
        {
            return string.Equals(_className+_methodName, b._className+ b._methodName);
        }

        public override bool Equals(object obj)
        {
            ActionItemObject a = obj as ActionItemObject;
            return a != null && Equals(a);
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(_className + _methodName) != null ? (_className + _methodName).GetHashCode() : base.GetHashCode();
        }
        public static bool operator ==(ActionItemObject a, Object b)
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
                return string.Equals(str, a.ClassName + a.MethodName, StringComparison.OrdinalIgnoreCase);
            }

            ActionItemObject tryToConvert = (ActionItemObject)b;
            if (tryToConvert == null) return false;

            return (a.ClassName + a.MethodName).Equals(tryToConvert.ClassName + tryToConvert.MethodName);
        }
        public static bool operator !=(ActionItemObject a, Object b)
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
                return !b.Equals(a.ClassName + a.MethodName);
            }

            ActionItemObject tryToConvert = (ActionItemObject)b;
            if (tryToConvert == null) return true;

            return !(a.ClassName + a.MethodName).Equals(tryToConvert.ClassName + tryToConvert.MethodName);
        }
    }
}

