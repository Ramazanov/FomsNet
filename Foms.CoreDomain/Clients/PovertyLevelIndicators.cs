using System;

namespace Foms.CoreDomain.Clients
{
    [Serializable]
    public class PovertyLevelIndicators
    {
        public int ChildrenEducation { get; set; }
        public int EconomicEducation { get; set; }
        public int SocialParticipation { get; set; }
        public int HealthSituation { get; set; }
    }
}
