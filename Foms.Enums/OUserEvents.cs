using System;

namespace Foms.Enums
{
    [Serializable]
    public static class OUserEvents
    {
        public static readonly string OctopusUserLogInEvent = "ULIE"; // Octopus User Log In Event
        public static readonly string OctopusUserLogOutEvent = "ULOE"; // Octopus User Log Out Event
        public static readonly string OctopusUserManualEntryEvent = "UMEE";
        public static readonly string OctopusUserStandardBookingEvent = "USBE";
        public static readonly string OctopusUserLoginDescription = "User connected";
        public static readonly string OctopusUserLogoutDescription = "User disconnected";
        public static readonly string OctopusUserManualEntryDescription = "Manual accounting entry event";
        public static readonly string OctopusUserStandardBookingDescription = "Manual standard booking event";
    }
}
