//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
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

namespace Foms.CoreDomain.Events
{
    [Serializable]
    public class StatisticalProvisionningEvent : Event
    {
        internal OCurrency   _OLBAmount;
        internal OCurrency   _provisionning;

        public override string Code
        {
            get { return "GOSE"; }
            set { _code = value; }
        }

        public OCurrency   OLBAmount
        {
            get { return _OLBAmount; }
            set { _OLBAmount = value; }
        }

        public OCurrency   Provisionning
        {
            get { return _provisionning; }
            set { _provisionning = value; }
        }

        public override string Description { get; set; }

        public override Event Copy()
        {
            return (StatisticalProvisionningEvent)MemberwiseClone();
        }
    }
}
