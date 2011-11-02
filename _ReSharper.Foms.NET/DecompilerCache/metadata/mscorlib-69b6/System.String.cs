// Type: System.String
// Assembly: mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
    [ComVisible(true)]
    [Serializable]
    public sealed class String : IComparable, ICloneable, IConvertible, IComparable<string>, IEnumerable<char>,
                                 IEnumerable, IEquatable<string>
    {
        public static readonly string Empty;

        [CLSCompliant(false)]
        public String(char* value);

        [CLSCompliant(false)]
        public String(char* value, int startIndex, int length);

        [CLSCompliant(false)]
        public String(sbyte* value);

        [CLSCompliant(false)]
        public String(sbyte* value, int startIndex, int length);

        [CLSCompliant(false)]
        public String(sbyte* value, int startIndex, int length, Encoding enc);

        public String(char[] value, int startIndex, int length);
        public String(char[] value);
        public String(char c, int count);

        [IndexerName("Chars")]
        public char this[int index] { get; }

        public int Length { get; }

        #region ICloneable Members

        public object Clone();

        #endregion

        #region IComparable Members

        public int CompareTo(object value);

        #endregion

        #region IComparable<string> Members

        public int CompareTo(string strB);

        #endregion

        #region IConvertible Members

        public string ToString(IFormatProvider provider);
        public TypeCode GetTypeCode();
        bool IConvertible.ToBoolean(IFormatProvider provider);
        char IConvertible.ToChar(IFormatProvider provider);
        sbyte IConvertible.ToSByte(IFormatProvider provider);
        byte IConvertible.ToByte(IFormatProvider provider);
        short IConvertible.ToInt16(IFormatProvider provider);
        ushort IConvertible.ToUInt16(IFormatProvider provider);
        int IConvertible.ToInt32(IFormatProvider provider);
        uint IConvertible.ToUInt32(IFormatProvider provider);
        long IConvertible.ToInt64(IFormatProvider provider);
        ulong IConvertible.ToUInt64(IFormatProvider provider);
        float IConvertible.ToSingle(IFormatProvider provider);
        double IConvertible.ToDouble(IFormatProvider provider);
        decimal IConvertible.ToDecimal(IFormatProvider provider);
        DateTime IConvertible.ToDateTime(IFormatProvider provider);
        object IConvertible.ToType(Type type, IFormatProvider provider);

        #endregion

        #region IEnumerable<char> Members

        IEnumerator<char> IEnumerable<char>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();

        #endregion

        #region IEquatable<string> Members

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public bool Equals(string value);

        #endregion

        public static bool operator ==(string a, string b);
        public static bool operator !=(string a, string b);
        public static string Join(string separator, string[] value);
        public static string Join(string separator, string[] value, int startIndex, int count);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public override bool Equals(object obj);

        public bool Equals(string value, StringComparison comparisonType);
        public static bool Equals(string a, string b);
        public static bool Equals(string a, string b, StringComparison comparisonType);
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);
        public char[] ToCharArray();
        public char[] ToCharArray(int startIndex, int length);
        public static bool IsNullOrEmpty(string value);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public override int GetHashCode();

        public string[] Split(params char[] separator);
        public string[] Split(char[] separator, int count);

        [ComVisible(false)]
        public string[] Split(char[] separator, StringSplitOptions options);

        [ComVisible(false)]
        public string[] Split(char[] separator, int count, StringSplitOptions options);

        [ComVisible(false)]
        public string[] Split(string[] separator, StringSplitOptions options);

        [ComVisible(false)]
        public string[] Split(string[] separator, int count, StringSplitOptions options);

        public string Substring(int startIndex);
        public string Substring(int startIndex, int length);
        public string Trim(params char[] trimChars);
        public string TrimStart(params char[] trimChars);
        public string TrimEnd(params char[] trimChars);
        public bool IsNormalized();
        public bool IsNormalized(NormalizationForm normalizationForm);
        public string Normalize();
        public string Normalize(NormalizationForm normalizationForm);
        public static int Compare(string strA, string strB);
        public static int Compare(string strA, string strB, bool ignoreCase);
        public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options);

        public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture,
                                  CompareOptions options);

        public static int Compare(string strA, string strB, StringComparison comparisonType);
        public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture);
        public static int Compare(string strA, int indexA, string strB, int indexB, int length);
        public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase);

        public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase,
                                  CultureInfo culture);

        public static int Compare(string strA, int indexA, string strB, int indexB, int length,
                                  StringComparison comparisonType);

        public static int CompareOrdinal(string strA, string strB);
        public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length);
        public bool Contains(string value);
        public bool EndsWith(string value);

        [ComVisible(false)]
        public bool EndsWith(string value, StringComparison comparisonType);

        public bool EndsWith(string value, bool ignoreCase, CultureInfo culture);
        public int IndexOf(char value);
        public int IndexOf(char value, int startIndex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public int IndexOf(char value, int startIndex, int count);

        public int IndexOfAny(char[] anyOf);
        public int IndexOfAny(char[] anyOf, int startIndex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public int IndexOfAny(char[] anyOf, int startIndex, int count);

        public int IndexOf(string value);
        public int IndexOf(string value, int startIndex);
        public int IndexOf(string value, int startIndex, int count);
        public int IndexOf(string value, StringComparison comparisonType);
        public int IndexOf(string value, int startIndex, StringComparison comparisonType);
        public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType);
        public int LastIndexOf(char value);
        public int LastIndexOf(char value, int startIndex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public int LastIndexOf(char value, int startIndex, int count);

        public int LastIndexOfAny(char[] anyOf);
        public int LastIndexOfAny(char[] anyOf, int startIndex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public int LastIndexOfAny(char[] anyOf, int startIndex, int count);

        public int LastIndexOf(string value);
        public int LastIndexOf(string value, int startIndex);
        public int LastIndexOf(string value, int startIndex, int count);
        public int LastIndexOf(string value, StringComparison comparisonType);
        public int LastIndexOf(string value, int startIndex, StringComparison comparisonType);
        public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType);
        public string PadLeft(int totalWidth);
        public string PadLeft(int totalWidth, char paddingChar);
        public string PadRight(int totalWidth);
        public string PadRight(int totalWidth, char paddingChar);
        public bool StartsWith(string value);

        [ComVisible(false)]
        public bool StartsWith(string value, StringComparison comparisonType);

        public bool StartsWith(string value, bool ignoreCase, CultureInfo culture);
        public string ToLower();
        public string ToLower(CultureInfo culture);
        public string ToLowerInvariant();
        public string ToUpper();
        public string ToUpper(CultureInfo culture);
        public string ToUpperInvariant();
        public override string ToString();
        public string Trim();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public string Insert(int startIndex, string value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public string Replace(char oldChar, char newChar);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public string Replace(string oldValue, string newValue);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public string Remove(int startIndex, int count);

        public string Remove(int startIndex);
        public static string Format(string format, object arg0);
        public static string Format(string format, object arg0, object arg1);
        public static string Format(string format, object arg0, object arg1, object arg2);
        public static string Format(string format, params object[] args);
        public static string Format(IFormatProvider provider, string format, params object[] args);
        public static string Copy(string str);
        public static string Concat(object arg0);
        public static string Concat(object arg0, object arg1);
        public static string Concat(object arg0, object arg1, object arg2);

        [CLSCompliant(false)]
        public static string Concat(object arg0, object arg1, object arg2, object arg3);

        public static string Concat(params object[] args);
        public static string Concat(string str0, string str1);
        public static string Concat(string str0, string str1, string str2);
        public static string Concat(string str0, string str1, string str2, string str3);
        public static string Concat(params string[] values);
        public static string Intern(string str);
        public static string IsInterned(string str);
        public CharEnumerator GetEnumerator();
    }
}
