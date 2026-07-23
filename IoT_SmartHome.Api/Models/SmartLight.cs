namespace IoT_SmartHome.Api.Models
{
    public class SmartLight : SmartDevice
    {
        // The current brightness level (0 - 100)
        public int Brightness { get; set; }

        // The maximum wattage of the bulb
        public int MaxWattage { get; set; }

        // Is the light currently on?
        public bool IsOn { get; set; }

        // Calculate the power draw based on brightness 
        public double CurrentPowerUsage => IsOn ? (MaxWattage * (Brightness/100.0)) : 0;

    }
}
