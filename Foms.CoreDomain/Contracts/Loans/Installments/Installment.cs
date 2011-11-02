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

namespace Foms.CoreDomain.Contracts.Loans.Installments
{
    /// <summary>
    /// Description r�sum�e de Installment.
    /// </summary>
    [Serializable]
    public class Installment : IInstallment
    {
        private OCurrency _interestsRepayment;
        private OCurrency _capitalRepayment;
        private OCurrency _paidInterests = 0;
        private OCurrency _paidCapital = 0;
        private OCurrency _paidFees = 0;
        private OCurrency _paidCommision = 0;
        private OCurrency _unpaidCommision = 0;
        private OCurrency _olbAfterRepayment = 0;
        private bool _pending = false;
        
        public Installment() {}
        public Installment(DateTime pExpectedDate,OCurrency pInterestRepayment,OCurrency pCapitalRepayment,
                           OCurrency pAidCapital, OCurrency pAidInterests, OCurrency pAidFees, DateTime? pAidDate, int pNumber)
        {
            ExpectedDate = pExpectedDate;
            _interestsRepayment = pInterestRepayment;
            _capitalRepayment = pCapitalRepayment;
            _paidCapital = pAidCapital;
            _paidInterests = pAidInterests;
            _paidFees = pAidFees;
            PaidDate = pAidDate;
            Number = pNumber;
        }

        public DateTime ExpectedDate { get; set; }

        public OCurrency FeesUnpaid { get; set; }

        public bool NotPaidYet { get; set; }

        public OPaymentMethods? PaymentMethod { get; set; }
        public string Comment { get; set; }
        public bool IsPending
        {
            get { return _pending; }
            set { _pending = value; }
        }

        public OCurrency InterestsRepayment
        {
            get{ return _interestsRepayment;}
            set{_interestsRepayment = value;}
        }

        public OCurrency PaidFees
        {
            get { return _paidFees; }
            set { _paidFees = value; }
        }

        public OCurrency PaidCommissions
        {
            get { return _paidCommision; }
            set { _paidCommision = value; }
        }

        public OCurrency CommissionsUnpaid
        {
            get { return _unpaidCommision; }
            set { _unpaidCommision = value; }
        }

        public OCurrency CapitalRepayment
        {
            get{return _capitalRepayment;}
            set{_capitalRepayment = value;}
        }

        public OCurrency   PaidCapital
        {
            get{return _paidCapital;}
            set{_paidCapital = value;}
        }

        public OCurrency   PaidInterests
        {
            get{return _paidInterests;}
            set{_paidInterests = value;}
        }

        public DateTime? PaidDate { get; set; }

        public int Number { get; set; }

        public OCurrency Amount
        {
            get
            {
                return  _interestsRepayment + _capitalRepayment;
            }
        }

        public OCurrency   AmountHasToPayWithInterest
        {
            get 
            { 
                return AmountComparer.Compare(_interestsRepayment + _capitalRepayment - _paidCapital - _paidInterests, 0) == 0
                           ? 0
                           : _interestsRepayment + _capitalRepayment - _paidCapital - _paidInterests;
            }
        }

        public OCurrency   PrincipalHasToPay
        {
            get {
                return AmountComparer.Compare(_capitalRepayment - _paidCapital, 0) == 0
                           ? 0
                           : _capitalRepayment - _paidCapital;
            }
        }

        public OCurrency   InterestHasToPay
        {
            get {
                return AmountComparer.Compare(_interestsRepayment - _paidInterests, 0) == 0
                           ? 0
                           : _interestsRepayment - _paidInterests;
            }
        }


        /// <summary>
        /// OLB is the OLB before installment repayment
        /// </summary>
        public OCurrency OLB { get; set; }
        public OCurrency OLBAfterRepayment
        {
            get{return OLB - CapitalRepayment;}
            set{_olbAfterRepayment = value;}
        }

        /// <summary>
        /// This method determines if an installment is fully repaid
        /// Business rule : to be considered as repaid, an installment must have interestsRepayment and capitalRepayment equal to
        /// paidCapital and paidInterests (effective amounts paid by the client)
        /// </summary>
        public bool IsRepaid
        {
            get
            {
                return AmountComparer.Equals(_interestsRepayment, _paidInterests) && AmountComparer.Equals(Math.Round(_capitalRepayment.Value, 2, MidpointRounding.AwayFromZero), _paidCapital) ? true : false;
            }
        }

        public bool IsPartiallyRepaid
        {
            get
            {
                OCurrency amount = _capitalRepayment + _interestsRepayment - _paidInterests - _paidCapital;
                return amount != 0 && !AmountComparer.Equals(amount, _capitalRepayment + _interestsRepayment);
            }
        }

        public Installment Copy()
        {
            return (Installment)MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            Installment compareTo = obj as Installment;
            if (null == compareTo) return false;
            if (Number != compareTo.Number) return false;
            if (CapitalRepayment != compareTo.CapitalRepayment) return false;
            if (InterestsRepayment != compareTo.InterestsRepayment) return false;
            if (!ExpectedDate.Equals(compareTo.ExpectedDate)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return Number;
        }
    }
}