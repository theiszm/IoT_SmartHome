namespace IoT_SmartHome.Api.Models
{
    public class SmartSecurityCamera : SmartDevice
    {
        public bool IsRecording { get; set; }
        public int StorageUsagePercentage { get; set; }
        public bool MotionDetected { get; set; }
    }
}
