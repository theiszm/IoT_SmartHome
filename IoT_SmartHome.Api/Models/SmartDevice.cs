namespace IoT_SmartHome.Api.Models
{
    public abstract class SmartDevice
    {
        
        // Unique ID for the database
        public int Id { get; set; }

        // Where it is located in the house (e.g. living room, kitchen)
        public string Room { get; set; } = string.Empty;

        // Is the device currently reachable?
        public bool IsOnline { get; set; }

        // When was it last seen on the system?
        public DateTime LastUpdated { get; set; }
    
    }
}
