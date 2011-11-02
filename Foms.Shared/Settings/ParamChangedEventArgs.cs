using System;

namespace Foms.Shared.Settings
{
    public class ParamChangedEventArgs : EventArgs
    {
        private readonly string _key = string.Empty;
        private readonly object _value = null;

        public ParamChangedEventArgs(string pKey, object pValue)
        {
            _key = pKey;
            _value = pValue;
        }

        public string Key
        {
            get { return _key; }
        }

        public object Value
        {
            get { return _value; }
        }
    }
}