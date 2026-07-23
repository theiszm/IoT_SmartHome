namespace IoT_SmartHome.Api.Models
{
    public class SmartThermostat : SmartDevice
    {
        public double CurrentTemperature { get; set; }
        public double TargetTemperature { get; set; }
        public string SystemMode { get; set; } = "Off"; // Off, Heating, Cooling, Auto
    }
}
