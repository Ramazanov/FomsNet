﻿using System;
using System.Globalization;

namespace Foms.CoreDomain.Export.FieldType
{
    [Serializable]
    public class DateFieldType : IFieldType
    {
        public string StringFormat { get; set; }

        public DateFieldType()
        {
            StringFormat = "ddMMyy";
        }

        #region IFieldType Members

        public bool AlignRight { get; set; }

        public string Format(object o, int? length)
        {
            var d = (DateTime)o;
            var s = d.ToString(StringFormat);
            if (length.HasValue)
                return StringFieldType.Format(s, length.Value, AlignRight);
            else
                return s;
        }

        public object Parse(string s)
        {
            return DateTime.ParseExact(s, StringFormat, CultureInfo.InvariantCulture);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
