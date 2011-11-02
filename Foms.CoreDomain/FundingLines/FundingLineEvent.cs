﻿using System;
using Foms.CoreDomain.Events;

namespace Foms.CoreDomain.FundingLines
{
    [Serializable]
    public class FundingLineEvent : Event
    {
        public new int Id { get; set; }

        private OCurrency _amount = decimal.Zero;
        private new string _code = string.Empty;
        private bool _isDelete = false;
        private OBookingDirections _movement = OBookingDirections.Credit;
        private OFundingLineEventTypes _type = OFundingLineEventTypes.Entry;
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public override string Description { get; set; }
        private FundingLine _fundingLine;

        public OFundingLineEventTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public bool IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        public override string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public OBookingDirections Movement
        {
            get { return _movement; }
            set { _movement = value; }
        }

        public OCurrency Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public FundingLine FundingLine
        {
            get { return _fundingLine; }
            set { _fundingLine = value; }
        }

        public override Event Copy()
        {
            return (FundingLineEvent) MemberwiseClone();
        }

        public Event AttachTo { get; set; }
    }
}