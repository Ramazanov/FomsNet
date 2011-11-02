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
using System.Linq;

namespace Foms.CoreDomain
{
    /// <summary>
    /// Description rйsumйe de Role.
    /// </summary>
    /// 
    [Serializable]
    public class Role : IEquatable<Role>
    {
        private int id;
        private string roleName;
        private bool _isDeleted;
        private string description;
        private Dictionary<MenuObject, bool> _menus;
        private Dictionary<ActionItemObject, bool> _actions;

        public Role()
        {
            _menus = new Dictionary<MenuObject, bool>();
            _actions = new Dictionary<ActionItemObject, bool>();
        }
        public Dictionary<MenuObject, bool> GetMenuItems()
        {
            return _menus; 
        }
        public Dictionary<ActionItemObject, bool> GetActionItems()
        {
            return _actions;
        }       
        public Dictionary<ActionItemObject, bool> GetSortedActionItems()
        {
            var sortedDict = (from entry in _actions orderby entry.Key.ClassName ascending select entry);
            return sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public void SetActionItems(Dictionary<ActionItemObject, bool> pActions)
        {
            _actions = pActions;
        }
        public void SetMenuItems(Dictionary<MenuObject, bool> pMenus)
        {
            _menus = pMenus;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }    

        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

 

        public string Description
        {
            get { return description; }
            set { description = value;}
        }

        public override string ToString()
        {
            return roleName;
        }
        public override bool Equals(object b)
        {
            if (b is string)
            {
                if (roleName.Equals(b))
                    return true;

            }
            Role a = b as Role;
            return a != null && Equals(a);
        }
        public bool IsMenuAllowed(MenuObject pMenu)
        {
            bool allowed;
            if (_menus.TryGetValue(pMenu, out allowed))
            {
                return _menus[pMenu];
            }
            pMenu.NotSavedInDBYet = true;
            _menus.Add(pMenu, true);
            return true;
        }
        public bool IsActionAllowed(ActionItemObject pAction)
        {
            if (_actions.ContainsKey(pAction))
            {
                return _actions[pAction];
            }
            return true;
        }
        public void SetMenuAllowed(MenuObject pMenu, bool pAllowed)
        {
             if(_menus.ContainsKey(pMenu))             
                 _menus[pMenu] = pAllowed;
             else
                 _menus.Add(pMenu, pAllowed);
        }
        public void SetActionAllowed(ActionItemObject pAction, bool pAllowed)
        {
            if (_actions.ContainsKey(pAction))
                _actions[pAction] = pAllowed;
            else
                _actions.Add(pAction, pAllowed);
        }
        public static bool operator ==(Role a, System.Object b)
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
                return b.Equals(a.roleName);
            }

            Role tryToConvert = (Role)b;
            if (tryToConvert == null)
                return false;

            return a.roleName.Equals(tryToConvert.roleName);
        }
        public static bool operator !=(Role a, System.Object b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return false;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || (b == null))
            {
                return true;
            }
            if (b is string)
            {
                return !b.Equals(a.roleName);
            }

            Role tryToConvert = (Role)b;
            if (tryToConvert == null)
                return true;

            return !a.roleName.Equals(tryToConvert.roleName);
        }
        public bool Equals(Role b)
        {
            return string.Equals(roleName, b.roleName);
        }

 

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(roleName) != null ? roleName.GetHashCode() : base.GetHashCode();
        }
     }
}