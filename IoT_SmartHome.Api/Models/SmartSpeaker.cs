namespace IoT_SmartHome.Api.Models
{
    public class SmartSpeaker : SmartDevice
    {
        public int  Volume { get; set; }    // 0 - 100
        public string CurrentTrack { get; set; } = string.Empty;
        public bool IsMuted { get; set; }
    }
}
