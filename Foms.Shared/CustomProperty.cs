using System;

namespace Foms.Shared
{
    /// <summary>
    /// Custom property class 
    /// </summary>
    public class CustomProperty
    {
        private string sName = string.Empty;
        private string sDesc = string.Empty;
        private bool bReadOnly = false;
        private bool bVisible = true;
        private object objValue = null;

        public CustomProperty(string sName, string sDesc, object value, Type type, bool bReadOnly, bool bVisible)
        {
            this.sName = sName;
            this.sDesc = sDesc;
            this.objValue = value;
            this.type = type;
            this.bReadOnly = bReadOnly;
            this.bVisible = bVisible;
        }

        private Type type;
        public Type Type
        {
            get { return type; }
        }

        public bool ReadOnly
        {
            get
            {
                return bReadOnly;
            }
        }

        public string Name
        {
            get
            {
                return sName;
            }
        }

        public string Description
        {
            get
            {
                return sDesc;
            }
        }

        public bool Visible
        {
            get
            {
                return bVisible;
            }
        }

        public object Value
        {
            get
            {
                return objValue;
            }
            set
            {
                objValue = value;
            }
        }

    }
}