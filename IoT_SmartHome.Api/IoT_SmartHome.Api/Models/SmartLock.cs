namespace IoT_SmartHome.Api.Models
{
    public class SmartLock : SmartDevice
    {
        public bool IsLocked { get; set; } = true;

        public int BatteryPercentage { get; set; } = 100;

        public DateTime? LastUnlockedTime { get; set; }
    }
}
