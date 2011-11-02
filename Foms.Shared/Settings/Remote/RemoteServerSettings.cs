namespace Foms.Shared.Settings.Remote
{
    public class RemoteServerSettings
    {
        public static IRemoteServerSettings GetSettings()
        {
            return new XmlRemoteServerSettings();
        }
    }
}
