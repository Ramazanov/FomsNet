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
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Declining
{
    /// <summary>
    /// Summary description for DecliningKeepExpectedInstallments.
    /// </summary>
    [Serializable]
    public class DecliningKeepExpectedInstallments : IRepayNextInstallments
    {
        private readonly Loan _contract;
        private readonly RepayFeesStrategy _methodToRepayFees;
        private readonly RepayCommisionStrategy _methodToRepayCommission;
        private readonly RepayInterestStrategy _methodToRepayInterest;

        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public DecliningKeepExpectedInstallments(Loan pContract, CreditContractOptions pCco)
        {
            _paidInstallments = new List<Installment>();
            _contract = pContract;
            _methodToRepayInterest = new RepayInterestStrategy(pCco);
            _methodToRepayFees = new RepayFeesStrategy(pCco);
            _methodToRepayCommission = new RepayCommisionStrategy(pCco);
        }

        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            if (amountPaid == 0) return;
            for(int i = 0 ; i < _contract.NbOfInstallments ; i++)
            {
                Installment installment = _contract.GetInstallment(i);
                if (!installment.IsRepaid && amountPaid > 0)
                {
                    //commission
                    _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);
                    if (amountPaid == 0) break;

                    //penalty
                    _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);
                    if (amountPaid == 0) break;

                    //Interests
                    if (amountPaid == 0) return;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,ref interestPrepayment);

                    // Principal
                    if (amountPaid == 0)
                    {
                        _paidInstallments.Add(installment);
                        return;
                    }

                    if (AmountComparer.Compare(amountPaid, installment.CapitalRepayment - installment.PaidCapital) > 0)
                    {
                        OCurrency  principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;
                        installment.PaidCapital = installment.CapitalRepayment;
                        amountPaid -= principalHasToPay;
                        principalEvent += principalHasToPay;
                    }
                    else
                    {
                        installment.PaidCapital += amountPaid;
                        installment.PaidCapital = Math.Round(installment.PaidCapital.Value, 2);
                        principalEvent += amountPaid;
                        amountPaid = 0;
                    }

                    _paidInstallments.Add(installment);
                }
            }
        }
    }
}