namespace Foms.CoreDomain.Events
{
    public class AuditTrailEvent : Event
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string CodeId { get; set; }
    }
}