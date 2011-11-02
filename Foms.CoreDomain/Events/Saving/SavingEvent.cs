﻿//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
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

namespace Foms.CoreDomain.Events.Saving
{
    [Serializable]
	public abstract class SavingEvent : Event, IComparable, ISavingsFees
	{
		public override string Code { get; set; }
		public OCurrency Amount { get; set; }
        public OCurrency Fee { get; set; }
        public Type ProductType { get; set; }
        public OSavingsMethods? SavingsMethod { get; set; }
        public bool IsPending { get; set; }
        public int? PendingEventId { get; set; }

		public override Event Copy()
		{
			return (SavingEvent)MemberwiseClone();
		}

		#region IComparable Members

		public static bool operator >(SavingEvent a, SavingEvent b)
		{
			return a.Date.CompareTo(b.Date) > 0;
		}

		public static bool operator <(SavingEvent a, SavingEvent b)
		{
			return a.Date.CompareTo(b.Date) < 0;
		}

		int IComparable.CompareTo(object obj)
		{
			return Date.CompareTo(((SavingEvent)obj).Date);
        }
        #endregion

        public virtual OCurrency GetAmountForBalance()
        {
            return 0m;
        }

        public virtual OCurrency GetFeeForBalance()
        {
            return 0m;
        }

        public OCurrency GetBalanceChunk(DateTime date)
        {
            if (Deleted || Date > date) 
                return 0m;

            OCurrency retval = 0m;
            retval += GetAmountForBalance();
            retval += GetFeeForBalance();
            return retval;
        }
	}

    [Serializable]
    public abstract class SavingPositiveEvent : SavingEvent
    {
        public override sealed OCurrency GetAmountForBalance()
        {
            return Amount;
        }
    }

    [Serializable]
    public abstract class SavingNegativeEvent : SavingEvent
    {
        public override sealed OCurrency GetAmountForBalance()
        {
            if (!Amount.HasValue) return 0m;
            return (decimal) -1*Amount;
        }

        public override sealed OCurrency GetFeeForBalance()
        {
            if (!Fee.HasValue) return 0m;
            return (decimal) -1*Fee;
        }
    }
}