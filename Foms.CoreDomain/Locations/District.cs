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

namespace Foms.CoreDomain.Locations
{
	/// <summary>
	/// Summary description for DistrictArea.
    /// </summary>
    [Serializable]
	public class District
	{
		private int _id;
		private string _name;
		private Province _province;
        private bool _deleted = false;

        public District()
		{

		}

		public District(int id,string name)
		{
			_id = id;
			_name = name;
		}

		public District(int id,string name,Province province)
		{
			_id = id;
			_name = name;
			_province = province;
		}

		public District(string name,Province province)
		{
			_name = name;
			_province = province;
		}

		public int Id
		{
			get
			{
				return _id;	
			}
			set
			{
				_id = value;
			}
		}
		
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public Province Province
		{
			get
			{
				return _province;
			}
			set
			{
				_province = value;
			}
		}

        public bool Deleted
        {
            get
            {
                return _deleted;
            }
            set
            {
                _deleted = value;
            }
        }
		
		public override string ToString()
		{
			return _name;
		}
	}
}
