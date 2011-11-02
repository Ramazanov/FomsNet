﻿using System;

namespace Foms.CoreDomain.Clients
{
    [Serializable]
    public class FollowUp
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public OCurrency CA { get; set; }
        public int Jobs1 { get; set; }
        public int Jobs2 { get; set; }
        public string PersonalSituation { get; set; }
        public string Activity { get; set; }
        public string Comment { get; set; }
    }
}
