namespace Entities.RequestObject
{
    public class SettingPasswordRequest
    {
        public string? OldPass { get; set; }
        public string? NewPass { get; set; }
        public string? ReEnterPass { get; set; }
    }
}
