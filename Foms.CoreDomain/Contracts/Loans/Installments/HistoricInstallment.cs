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

namespace Foms.CoreDomain.Contracts.Loans.Installments
{
    /// <summary>
    /// Installment Historic.
    /// </summary>
    [Serializable]
    public class HistoricInstallment : IInstallment
    {
        public HistoricInstallment()
        {
            PaidInterests = 0;
            PaidCapital = 0;
            PaidFees = 0;
            IsPending = false;
        }

        public DateTime ExpectedDate { get; set; }

        public OCurrency InterestsRepayment { get; set; }

        public OCurrency CapitalRepayment { get; set; }

        public OCurrency PaidCapital { get; set; }

        public OCurrency PaidInterests { get; set; }

        public OCurrency PaidFees { get; set; }

        public DateTime? PaidDate { get; set; }

        public int Number { get; set; }

        public OPaymentMethods? PaymentMethod { get; set; }
        public string Comment { get; set; }
        public bool IsPending { get; set; }

        public OCurrency Amount
        {
            get
            {
                return InterestsRepayment + CapitalRepayment;
            }
        }

        public DateTime? ReschedulingDate { get; set; }

        public int EventId { get; set; }

        /// <summary>
        /// This method determines if an installment is fully repaid
        /// Business rule : to be considered as repaid, an installment must have interestsRepayment and capitalRepayment equal to
        /// paidCapital and paidInterests (effective amounts paid by the client)
        /// </summary>
        public bool IsRepaid
        {
            get
            {
                return InterestsRepayment == PaidInterests && CapitalRepayment == PaidCapital ? true : false;
            }
        }

        public int ContractId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}