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
using System.Runtime.Serialization;

namespace Foms.ExceptionsHandler.Exceptions.SavingExceptions
{
	/// <summary>
	/// Summary description for OctopusSavingException.
	/// </summary>
	[Serializable]
	public class OctopusSavingProductException : OctopusException
	{
        private readonly string _message;
        private OctopusSavingProductExceptionEnum _code;
        public OctopusSavingProductExceptionEnum Code { get { return _code; } }

        protected OctopusSavingProductException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = (OctopusSavingProductExceptionEnum)info.GetInt32("Code");
        }

        protected OctopusSavingProductException(SerializationInfo info, StreamingContext context, List<string> options)
            : base(info, context)
        {
            _code = (OctopusSavingProductExceptionEnum)info.GetInt32("Code");
            AdditionalOptions = options;
        }


		public OctopusSavingProductException()
		{
            _message = string.Empty;
		}

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OctopusSavingProductException(OctopusSavingProductExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = FindException(exceptionCode);
        }

        public OctopusSavingProductException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}

        private static string FindException(OctopusSavingProductExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OctopusSavingProductExceptionEnum.NameIsEmpty:
                    returned = "SavingProductNameIsEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DuplicateProductName:
                    returned = "SavingProductDuplicateProductName.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DuplicateProductCode:
                    returned = "SavingProductDuplicateProductCode.Text";
                    break;

                case OctopusSavingProductExceptionEnum.BalanceIsInvalid:
                    returned = "SavingProductBalanceIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountIsInvalid:
                    returned = "SavingProductInitialAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawAmountIsInvalid:
                    returned = "SavingProductWithdrawAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositAmountIsInvalid:
                    returned = "SavingProductDepositAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestRateMinMaxIsInvalid:
                    returned = "SavingProductInterestRateMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositMinAmountIsInvalid:
                    returned = "SavingProductDepositMinAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestRateIsInvalid:
                    returned = "SavingProductInterestRateIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawMinAmountIsInvalid:
                    returned = "SavingProductWithdrawMinAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMinIsInvalid:
                    returned = "SavingProductInitialAmountMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMinMaxIsInvalid:
                    returned = "SavingProductInitialAmountMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMaxNotInBalance:
                    returned = "SavingProductInitialAmountMaxNotInBalance.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMinNotInBalance:
                    returned = "SavingProductInitialAmountMinNotInBalance.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestsFrequencyIsInvalid:
                    returned = "SavingProductInterestsFrequencyIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestsBaseIsInvalid:
                    returned = "SavingProductInterestsBaseIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.ClientTypeIsInvalid:
                    returned = "ClientTypeIsEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CalculAmountBaseIsNull:
                    returned = "SavingProductCalculAmountBaseIsNull.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestBaseIncompatibleFrequency:
                    returned = "SavingProductInterestBaseIncompatibleFrequency.Text";
                    break;

                case OctopusSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid:
                    returned = "SavingProductEntryFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.EntryFeesIsInvalid:
                    returned = "SavingProductEntryFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.NumberPeriodIsInvalid:
                    returned = "SavingProductNumberPeriodIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.NumberPeriodMinIsInvalid:
                    returned = "SavingProductNumberPeriodMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.NumberPeriodMinMaxIsInvalid:
                    returned = "SavingProductNumberPeriodMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawFeesTypeEmpty:
                    returned = "SavingProductWithdrawFeesTypeEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatWithdrawFeesIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatWithdrawFeesMinIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatWithdrawFeesMinMaxIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateWithdrawFeesIsInvalid:
                    returned = "SavingProductRateWithdrawFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateWithdrawFeesMinIsInvalid:
                    returned = "SavingProductRateWithdrawFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateWithdrawFeesMinMaxIsInvalid:
                    returned = "SavingProductRateWithdrawFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.TransferAmountIsInvalid:
                    returned = "SavingProductTransferAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.TransferAmountMinIsInvalid:
                    returned = "SavingProductTransferAmountMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawalFeesMinMaxIsInvalid:
                    returned = "SavingProductWithdrawalFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawalFeesIsInvalid:
                    returned = "SavingProductWithdrawalFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawalFeesMinIsInvalid:
                    returned = "SavingProductWithdrawalFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CodeIsEmpty:
                    returned = "SavingProductCodeIsEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.TransferFeesTypeEmpty:
                    returned = "SavingProductTransferFeesTypeEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatTransferFeesIsInvalid:
                    returned = "SavingProductFlatTransferFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatTransferFeesMinIsInvalid:
                    returned = "SavingProductFlatTransferFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductFlatTransferFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateTransferFeesIsInvalid:
                    returned = "SavingProductRateTransferFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateTransferFeesMinIsInvalid:
                    returned = "SavingProductRateTransferFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductRateTransferFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.LoanAmountIsInvalid:
                    returned = "SavingProductLoanAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.LoanAmountMinIsInvalid:
                    returned = "SavingProductLoanAmountMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.LoanAmountMinMaxIsInvalid:
                    returned = "SavingProductLoanAmountMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositFeesIsInvalid:
                    returned = "SavingProductDepositFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositFeesMinIsInvalid:
                    returned = "SavingProductDepositFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CloseFeesIsInvalid:
                    returned = "SavingProductCloseFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CloseFeesMinIsInvalid:
                    returned = "SavingProductCloseFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.ManagementFeesIsInvalid:
                    returned = "SavingProductManagementFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.ManagementFeesMinIsInvalid:
                    returned = "SavingProductManagementFeesMinIsInvalid.Text";
                    break;
                case OctopusSavingProductExceptionEnum.PeriodicityIsEmpty:
                    returned = "SavingProductPeriodicityIsEmpty.Text";
                    break;
                case OctopusSavingProductExceptionEnum.OverdraftFeesIsInvalid:
                    returned = "OverdraftFeesIsInvalid.Text";
                    break;
                case OctopusSavingProductExceptionEnum.AgioFeesIsInvalid:
                    returned = "AgioFeesIsInvalid.Text";
                    break;
                case OctopusSavingProductExceptionEnum.ChequeDepositIsInvalid:
                    returned = "ChequeDepositIsInvalid";
                    break;
                case OctopusSavingProductExceptionEnum.ChequeDepositFeesIsInvalid:
                    returned = "ChequeDepositFeesIsInvalid";
                    break;
                case OctopusSavingProductExceptionEnum.ReopenFeesIsInvalid:
                    returned = "ReopenFeesIsInvalid";
                    break;
                case OctopusSavingProductExceptionEnum.ReopenFeesMinIsInvalid:
                    returned = "ReopenFeesMinIsInvalid";
                    break;
            }
            return returned;
        }
	}

	[Serializable]
	public enum OctopusSavingProductExceptionEnum
	{
		DuplicateProductName,
        DuplicateProductCode,
		InterestRateIsInvalid,
		BalanceIsInvalid,
		DepositMinAmountIsInvalid,
		DepositAmountIsInvalid,
        CodeIsEmpty,
		WithdrawAmountIsInvalid,
		WithdrawMinAmountIsInvalid,
		InterestRateMinIsInvalid,
		InterestRateMinMaxIsInvalid,
		InitialAmountIsInvalid,
        InitialAmountMinMaxIsInvalid,
		InitialAmountMinIsInvalid,
		InitialAmountMinNotInBalance,
		InitialAmountMaxNotInBalance,
        InterestsFrequencyIsInvalid,
        InterestsBaseIsInvalid,
        InterestBaseIncompatibleFrequency,
	    NameIsEmpty,
        CurrencyIsEmpty,
        ClientTypeIsInvalid,
        CalculAmountBaseIsNull,
        EntryFeesMinMaxIsInvalid,
        EntryFeesIsInvalid, 
        NumberPeriodIsInvalid,
        NumberPeriodMinIsInvalid,
        NumberPeriodMinMaxIsInvalid, 
        PeriodicityIsEmpty,
        WithdrawFeesTypeEmpty,
        FlatWithdrawFeesIsInvalid,
        FlatWithdrawFeesMinIsInvalid,
        FlatWithdrawFeesMinMaxIsInvalid,
        RateWithdrawFeesIsInvalid,
        RateWithdrawFeesMinIsInvalid,
        RateWithdrawFeesMinMaxIsInvalid,
        TransferAmountIsInvalid,
        TransferAmountMinIsInvalid,
        WithdrawalFeesMinMaxIsInvalid,
        WithdrawalFeesIsInvalid,
        WithdrawalFeesMinIsInvalid,
        TransferFeesTypeEmpty,
        FlatTransferFeesIsInvalid,
        FlatTransferFeesMinIsInvalid,
        FlatTransferFeesMinMaxIsInvalid,
        RateTransferFeesIsInvalid,
        RateTransferFeesMinIsInvalid,
        RateTransferFeesMinMaxIsInvalid,
        LoanAmountIsInvalid,
        LoanAmountMinIsInvalid,
        LoanAmountMinMaxIsInvalid,
        DepositFeesIsInvalid,
        DepositFeesMinIsInvalid,
        CloseFeesIsInvalid,
        CloseFeesMinIsInvalid,
        ManagementFeesIsInvalid,
        ManagementFeesMinIsInvalid,
        OverdraftFeesIsInvalid,
        OverdraftFeesMinIsInvalid,
        AgioFeesIsInvalid,
        AgioFeesMinIsInvalid,
        ChequeDepositIsInvalid,
        ChequeDepositFeesIsInvalid,
        ReopenFeesIsInvalid,
        ReopenFeesMinIsInvalid
	}
}
