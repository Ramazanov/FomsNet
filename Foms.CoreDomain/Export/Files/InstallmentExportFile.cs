﻿using System;
using System.Collections.Generic;
using Foms.CoreDomain.Export.FieldType;
using Foms.CoreDomain.Export.Fields;
using Foms.Enums;

namespace Foms.CoreDomain.Export.Files
{
    [Serializable]
    public class InstallmentExportFile : AExportFile<Installment>
    {
        public InstallmentExportFile()
        {
            HasFieldsSpecificLength = true;
            Extension = ".txt";
            SelectedFields = new List<IField>();
        }

        public bool TagInstallmentAsPending
        {
            get;
            set;
        }

        public OPaymentMethods? PaymentMethod
        {
            get;
            set;
        }

        public override List<IField> DefaultList
        {
            get
            {
                return new List<IField>
                {   
                    new Field { Name = "ContractCode", FieldType = new StringFieldType(), DefaultLength = 20},
                    new Field { Name = "InstallmentNumber", FieldType = new IntegerFieldType(), DefaultLength = 2},
                    new Field { Name = "InstallmentAmount", FieldType = new DecimalFieldType(), DefaultLength = 10},
                    new Field { Name = "InstallmentDate", FieldType = new DateFieldType(), DefaultLength = 6},
                    new Field { Name = "ClientName" , FieldType = new StringFieldType (), DefaultLength = 35},
                    new Field { Name = "PersonalBankName", FieldType = new StringFieldType(), DefaultLength = 35},
                    new Field { Name = "PersonalBankBIC", FieldType = new StringFieldType(), DefaultLength = 11},
                    new Field { Name = "PersonalBankIban1", FieldType = new StringFieldType(), DefaultLength = 34},
                    new Field { Name = "PersonalBankIban2", FieldType = new StringFieldType(), DefaultLength = 34},
                    new Field { Name = "BusinessBankName", FieldType = new StringFieldType(), DefaultLength = 35},
                    new Field { Name = "BusinessBankBIC", FieldType = new StringFieldType(), DefaultLength = 11},
                    new Field { Name = "BusinessBankIban1", FieldType = new StringFieldType(), DefaultLength = 34},
                    new Field { Name = "BusinessBankIban2", FieldType = new StringFieldType(), DefaultLength = 34},
                    new Field { Name = "ProductCode", FieldType = new StringFieldType(), DefaultLength = 10},
                    new Field { Name = "ProductName", FieldType = new StringFieldType(), DefaultLength = 20}
                };
            }
        }

        protected override string _getFormatedField(Installment pRowData, Field pField)
        {
            switch (pField.Name)
            {
                case "ContractCode": return pField.Format(pRowData.ContractCode);
                case "InstallmentNumber": return pField.Format(pRowData.InstallmentNumber);
                case "ClientName": return pField.Format(pRowData.ClientName);
                case "PersonalBankName": return pField.Format(pRowData.PersonalBankName);
                case "PersonalBankBIC": return pField.Format(pRowData.PersonalBankBic);
                case "PersonalBankIban1": return pField.Format(pRowData.PersonalBankIban1);
                case "PersonalBankIban2": return pField.Format(pRowData.PersonalBankIban2);
                case "BusinessBankName": return pField.Format(pRowData.BusinessBankName);
                case "BusinessBankBIC": return pField.Format(pRowData.BusinessBankBic);
                case "BusinessBankIban1": return pField.Format(pRowData.BusinessBankIban1);
                case "BusinessBankIban2": return pField.Format(pRowData.BusinessBankIban2);
                case "ProductCode": return pField.Format(pRowData.ProductCode);
                case "ProductName": return pField.Format(pRowData.ProductName);
                case "InstallmentAmount": return pField.Format(pRowData.InstallmentAmount.Value);
                case "InstallmentDate": return pField.Format(pRowData.InstallmentDate);
            }

            return string.Empty;
        }
    }
}
