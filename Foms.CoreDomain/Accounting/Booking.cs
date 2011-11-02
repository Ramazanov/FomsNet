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
using Foms.CoreDomain.Accounting.Interfaces;

namespace Foms.CoreDomain.Accounting
{
	/// <summary>
	/// Description r�sum�e de Booking.
    /// </summary>
    [Serializable]
	public class Booking : IBooking
	{
	    private OCurrency   _amount;
        public int Id { get; set; }
	    public OAccountingLabels Label { get; set; }
	    public int Number { get; set; }
	    public Account CreditAccount { get; set; }
        public string CreditAccountNumber { get { return CreditAccount.Number; } }
	    public bool IsExported { get; set; }
	    public Account DebitAccount { get; set; }
        public string DebitAccountNumber { get { return DebitAccount.Number; } }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Currency Currency { get; set; }
        public OCurrency ExchangeRate { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }

	    public Booking(){}

        /// <summary>
        /// use this constructor only if booking isn't a Repayment
        /// </summary>
        /// <param name="pNumber"></param>
        /// <param name="pCreditaccount"></param>
        /// <param name="pAmount"></param>
        /// <param name="pDebitaccount"></param>
        /// <param name="pDate"></param>
        public Booking(int pNumber, Account pCreditaccount, OCurrency pAmount, Account pDebitaccount, DateTime pDate)
        {
            Number = pNumber;
            CreditAccount = pCreditaccount;
            DebitAccount = pDebitaccount;
            _amount = pAmount;
            Label = OAccountingLabels.Nothing;
            Date = pDate;
        }

        public Booking(int pNumber, Account pCreditaccount, OCurrency pAmount, Account pDebitaccount, OAccountingLabels pLabel, DateTime pDate)
		{
            Number = pNumber;
            CreditAccount = pCreditaccount;
            DebitAccount = pDebitaccount;
            _amount = pAmount;
		    Label = pLabel;
            Date = pDate;
        }

	    public OCurrency Amount
		{
			get{return _amount;}
			set{_amount = value;}
        }

		public OCurrency AmountRounding
		{
            get{return Math.Round(_amount.Value,2);}
		}

        public override string ToString()
        {
            return Name + "  " + DebitAccount + " : " + CreditAccount;
        }
	}
}
