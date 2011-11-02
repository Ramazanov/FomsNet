using System;
using System.Globalization;

namespace Foms.Shared
{
	[Serializable]
	public struct OCurrency
	{
        private decimal? _currency;
        public static bool UseCents = true;

		public OCurrency(decimal a)
		{
			_currency = a;
		}
		public OCurrency(decimal? a)
		{
		    _currency = a.HasValue ? a : null;
		}

	    public OCurrency(OCurrency obj)
		{
			_currency = obj._currency;
		}

		public decimal Value
		{
			get { return _currency.Value; }
		}

		public string RoundingValue
		{
			get 
            {
                var val = _currency ?? 0;
                var roundPrecision = UseCents ? 2 : 0;
                var fmt = UseCents ? "{0:N2}" : "{0:N0}";
                val = Math.Round(val, roundPrecision, MidpointRounding.AwayFromZero);
                return string.Format(fmt, val);
			}
		}

        public string GetFormatedValue(bool useCents)
        {
            var val = _currency ?? 0;
            var roundPrecision = useCents ? 2 : 0;
            //var fmt = pUseCents ? "{0:N}" : "{0:G}";
            val = Math.Round(val, roundPrecision, MidpointRounding.AwayFromZero);

            // Gets a NumberFormatInfo
            NumberFormatInfo nfi = new CultureInfo(CultureInfo.CurrentCulture.ToString(), false).NumberFormat;
            //copying settings
            nfi.NumberGroupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            nfi.NumberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            nfi.CurrencyGroupSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
            nfi.CurrencyDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            nfi.NumberDecimalDigits = roundPrecision;

            return val.ToString("N", nfi);
        }

		public bool HasValue
		{
			get { return _currency.HasValue; }
		}

		public static implicit operator OCurrency(decimal? source)
		{
		    return source.HasValue ? new OCurrency(Math.Round(source.Value, 4, MidpointRounding.AwayFromZero)) : new OCurrency(source);
		}

	    public static OCurrency operator +(OCurrency a, OCurrency b)
	    {
	        return a.HasValue && b.HasValue
	                   ? new OCurrency(Math.Round((a._currency.Value + b._currency.Value), 4, MidpointRounding.AwayFromZero))
	                   : null;
	    }

	    public static OCurrency operator -(OCurrency a, OCurrency b)
	    {
	        return a.HasValue && b.HasValue
	                   ? new OCurrency(Math.Round((a._currency.Value - b._currency.Value), 4, MidpointRounding.AwayFromZero))
	                   : null;
	    }

	    public static OCurrency operator *(OCurrency a, OCurrency b)
	    {
	        return a.HasValue && b.HasValue
	                   ? new OCurrency(Math.Round((a._currency.Value*b._currency.Value), 4, MidpointRounding.AwayFromZero))
	                   : ((a.HasValue && a.Value == 0) | (b.HasValue && b.Value == 0) 
                                    ? (OCurrency) 0 
                                    : null);
	    }

	    public static OCurrency operator *(OCurrency a, double b)
	    {
	        if (!a.HasValue)
	            return null;

	        double resulDouble = Convert.ToDouble(a.Value)*b;
	        return new OCurrency(Math.Round(Convert.ToDecimal(resulDouble), 4, MidpointRounding.AwayFromZero));
	    }

	    public static OCurrency operator *(OCurrency a, int b)
	    {
	        if (!a.HasValue)
	            return null;

	        decimal resulDecimal = Convert.ToDecimal(a.Value) * b;
	        return new OCurrency(Math.Round(resulDecimal, 4, MidpointRounding.AwayFromZero));
	    }

	    public static OCurrency operator *(double a, OCurrency b)
		{
	        if (!b.HasValue)
	            return null;
	        
            double resulDouble = Convert.ToDouble(b.Value) * a;
	        return new OCurrency(Math.Round(Convert.ToDecimal(resulDouble), 4, MidpointRounding.AwayFromZero));
		}

	    public static OCurrency operator /(OCurrency a, OCurrency b)
		{
		    if (a.HasValue && b.HasValue)
			{
			    if (b.Value != 0)
					return new OCurrency(Math.Round(a._currency.Value / b._currency.Value, 4, MidpointRounding.AwayFromZero));
			    throw new OverflowException();
			}
		    return null;
		}

	    public static OCurrency operator /(OCurrency a, int b)
        {
            if (a.HasValue && b != 0)
            {
                return new OCurrency(Math.Round(a._currency.Value / b, 4, MidpointRounding.AwayFromZero));
            }
            return null;
        }

	    public static OCurrency operator /(OCurrency a, double b)
		{
			if (a.HasValue)
			{
			    if (b != 0)
					return new OCurrency(Math.Round(a._currency.Value / (decimal)b, 4, MidpointRounding.AwayFromZero));
			    throw new OverflowException();
			}
	        if (!a.HasValue)
	        {
	            if (b != 0)
					return null;
	            throw new OverflowException();
	        }
	        throw new ArgumentNullException("Occurrency does not have value");
		}

		public static bool operator >(OCurrency a, OCurrency b)
		{
		    if (a.HasValue && b.HasValue)
			{
				return a._currency.Value > b._currency.Value;
			}
		    return a.HasValue && !b.HasValue;
		}

	    public static bool operator <(OCurrency a, OCurrency b)
	    {
	        if (a.HasValue && b.HasValue)
			{
			    return a._currency.Value < b._currency.Value;
			}
	        return a.HasValue && !b.HasValue;
	    }

	    public static bool operator >=(OCurrency a, OCurrency b)
	    {
	        if (a.HasValue && b.HasValue)
			{
			    return a._currency.Value > b._currency.Value | a._currency.Value == b._currency.Value;
			}
	        return a.HasValue && !b.HasValue;
	    }

	    public static bool operator <=(OCurrency a, OCurrency b)
	    {
	        if (a.HasValue && b.HasValue)
	            return a._currency.Value < b._currency.Value | a._currency.Value == b._currency.Value;

	        return a.HasValue && !b.HasValue;
	    }

	    public static bool operator ==(OCurrency a, OCurrency b)
		{
		    if (a.HasValue && b.HasValue)
		        return a._currency.Value == b._currency.Value;
		    return !a.HasValue && !b.HasValue;
		}

	    public static bool operator !=(OCurrency a, OCurrency b)
		{
			if (a.HasValue && b.HasValue)
			{
			    return a._currency.Value > b._currency.Value | a._currency.Value < b._currency.Value;
			}
	        return a != null || b != null;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override bool Equals(object b)
		{
			if (HasValue)
			{
			    if(b is OCurrency)
    			    return Value.Equals(((OCurrency)b).Value);
			    
                return Value.Equals(Convert.ToDecimal(b));
			}
		    return !HasValue && b == null;
		}

		public override string ToString()
		{
		    return _currency.ToString();
		}
	}
}