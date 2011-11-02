namespace Foms.Shared.Settings.Remote
{
    public interface IRemoteServerSettings
    {
        string ServerName { get; set; }
        string LoginName { get; set; }
        string Password { get; set; }
    }
}
