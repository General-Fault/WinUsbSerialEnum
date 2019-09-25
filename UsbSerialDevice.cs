namespace WinUsbSerialEnum
{
    public class UsbSerialDevice
    {
        public string Vid { get; set; }
        public string Pid { get; set; }
        public string Rev { get; set; }
        public string FriendlyName { get; set; }
        public string PortName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{FriendlyName} [{PortName}]";
        }
    }
}
