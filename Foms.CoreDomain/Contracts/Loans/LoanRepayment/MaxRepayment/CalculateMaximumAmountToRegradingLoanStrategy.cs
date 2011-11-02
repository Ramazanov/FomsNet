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
using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment
{
    /// <summary>
    /// Summary description for MaximumAmountToRegradingLoanStrategy.
    /// </summary>
    [Serializable]
    public class CalculateMaximumAmountToRegradingLoanStrategy
    {
        private readonly Loan _contract;
        private readonly CreditContractOptions _cCo;
        private readonly User _user;
        private readonly ApplicationSettings _generalSettings;
        private readonly NonWorkingDateSingleton _nWDS;

        public CalculateMaximumAmountToRegradingLoanStrategy(CreditContractOptions pCCo,Loan pContract, User pUser, 
            ApplicationSettings pGeneralSettings,NonWorkingDateSingleton pNonWorkingDate)
        {
            _user = pUser;
            _generalSettings = pGeneralSettings;
            _nWDS = pNonWorkingDate;

            _contract = pContract;
            _cCo = pCCo;
        }

        public OCurrency CalculateMaximumAmountToRegradingLoan(DateTime pDate)
        {
            OCurrency amount = 0;
            //capital
            amount += _contract.CalculateActualOlb(pDate);

            //interest
            if (_cCo.CancelInterests)
                amount += _cCo.ManualInterestsAmount;
            else 
                amount += _contract.CalculateRemainingInterests(pDate);

            //fees
            if (_cCo.CancelFees)
            {
                amount += _cCo.ManualFeesAmount;
                amount += _cCo.ManualCommissionAmount;
            }
            else
                amount += _CalculateLateAndAnticipatedFees(pDate);

            return _contract.UseCents ? amount : Math.Round(amount.Value, 0);
        }

        private OCurrency _CalculateLateAndAnticipatedFees(DateTime pDate)
        {
            OCurrency fees = 0;
            Loan contract = _contract.Copy();
            new Repayment.RepayLateInstallments.CalculateInstallments(_cCo, contract, _user, _generalSettings,_nWDS).CalculateNewInstallmentsWithLateFees(pDate);
            for (int i = 0; i < contract.NbOfInstallments; i++)
            {
                Installment installment = contract.GetInstallment(i);
                if (!installment.IsRepaid && installment.ExpectedDate <= pDate)
                {
                    fees += installment.FeesUnpaid;
                    installment.PaidCapital = installment.CapitalRepayment;
                    installment.PaidInterests = installment.InterestsRepayment;
                }
            }
            return fees;
        }
    }
}