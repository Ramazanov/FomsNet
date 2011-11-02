﻿using Foms.CoreDomain.Export.FieldType;

namespace Foms.CoreDomain.Export.Fields
{
    public class CustomField : IField
    {
        #region IField Members

        public string Name
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string Header
        {
            get { return DisplayName; }
            set { DisplayName = value; }
        }

        public int? Length
        {
            get;
            set;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        public string DefaultValue
        {
            get;
            set;
        }

        public string Format()
        {
            if (Length.HasValue)
                return StringFieldType.Format(DefaultValue, Length.Value, false);
            else
                return DefaultValue;
        }
    }
}
