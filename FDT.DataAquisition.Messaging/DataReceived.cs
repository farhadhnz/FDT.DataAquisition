namespace FDT.DataAquisition.Messaging
{
    public class DataReceived
    {
        public int DigitalTwinId { get; set; }
        public DateTime ReceivedTime { get; set; }
        public double DataValue { get; set; }
        public string Event { get; set; }
    }
}
