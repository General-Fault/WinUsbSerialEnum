using NUnit.Framework;
using System;
using System.Linq;
using WinUsbSerialEnum;

namespace Tests
{
    public class UsbSerialDeviceEnumeratorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(new[] { 0x2341, 0x2A03 }, new[] { 0x0043, 0x0001, 0x0243 })] //Arduino Uno
        [Ignore("Requires USB serial device to be attached.")]
        public void GetPortFromPidVidTest(int?[] vids, int?[] pids)
        {
            var devices = UsbSerialDeviceEnumerator.EnumerateDevices().ToList();

            devices.ForEach(device => Console.WriteLine($"Found {device.FriendlyName} on port {device.PortName} - VID:{device.Vid}, PID:{device.Pid}"));
            Assert.That(devices.Any(device => vids.Contains(device.Vid) && pids.Contains(device.Pid)), Is.True);
        }
    }
}