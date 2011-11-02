namespace Foms.CoreDomain
{
    public  class EntryFee
    {
        public EntryFee()
        {
            Id = null;
            NameOfFee = "";
            Min = null;
            Max = null;
            Value = null;
            IsRate = false;
            Index = -1;
        }
        public int? Id { get; set; }
        public string NameOfFee { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? Value { get; set; }
        public bool IsRate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdded { get; set; }
        public int Index { get; set; }
    }
}
