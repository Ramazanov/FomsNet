using System;

namespace Foms.CoreDomain.Export.FieldType
{
    public interface IFieldType : ICloneable
    {
        bool AlignRight { get; set; }
        string Format(object o, int? length);
        object Parse(string s);
    }
}
