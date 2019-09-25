using System;

using System.Runtime.InteropServices;
using System.Text;

namespace WinUsbSerialEnum
{
    static class NativeMethods
    {
        public static Guid GUID_DEVINTERFACE_COMPORT = new Guid(0x86e0d1e0, 0x8089, 0x11d0, 0x9c, 0xe4, 0x08, 0x00, 0x3e, 0x30, 0x1f, 0x73);
        public static Guid GUID_DEVINTERFACE_SERENUM_BUS_ENUMERATOR = new Guid(0x4D36E978, 0xE325, 0x11CE, 0xBF, 0xC1, 0x08, 0x00, 0x2B, 0xE1, 0x03, 0x18);
        public const int NO_ERROR = 0;
        public const long INVALID_HANDLE_VALUE = -1;
        public const int ERROR_NO_MORE_ITEMS = 259;

        [Flags]
        internal enum DiGetClassFlags : uint
        {
            DIGCF_DEFAULT = 0x00000001, // only valid with DIGCF_DEVICEINTERFACE
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010,
        }

        internal enum DeviceInfoKeyType : ulong
        {
            DIREG_DEV = 0x00000001, // Open/Create/Delete device key
            DIREG_DRV = 0x00000002, // Open/Create/Delete driver key
            DIREG_BOTH = 0x00000004  // Delete both driver and Device key
        }

        [Flags]
        internal enum DeviceInfoPropertyScope : uint
        {
            DICS_FLAG_GLOBAL = 0x00000001, // make change in all hardware profiles
            DICS_FLAG_CONFIGSPECIFIC = 0x00000002, // make change in specified profile only
            DICS_FLAG_CONFIGGENERAL = 0x00000004 // 1 or more hardware profile-specific changes to follow.
        }

        internal enum DeviceInfoRegistryKeyType : uint
        {
            DIREG_DEV = 0x00000001, // Open/Create/Delete device key
            DIREG_DRV = 0x00000002, // Open/Create/Delete driver key
            DIREG_BOTH = 0x00000004, // Delete both driver and Device key
        }

        [Flags]
        internal enum StandardAccessRights : uint
        {
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000,

            STANDARD_RIGHTS_REQUIRED = 0x000F0000,

            STANDARD_RIGHTS_READ = READ_CONTROL,
            STANDARD_RIGHTS_WRITE = READ_CONTROL,
            STANDARD_RIGHTS_EXECUTE = READ_CONTROL,

            STANDARD_RIGHTS_ALL = 0x001F0000,
        }

        [Flags]
        internal enum RegistrySpecificAccessRights : uint
        {
            KEY_QUERY_VALUE = 0x0001,
            KEY_SET_VALUE = 0x0002,
            KEY_CREATE_SUB_KEY = 0x0004,
            KEY_ENUMERATE_SUB_KEYS = 0x0008,
            KEY_NOTIFY = 0x0010,
            KEY_CREATE_LINK = 0x0020,
            KEY_WOW64_32KEY = 0x0200,
            KEY_WOW64_64KEY = 0x0100,
            KEY_WOW64_RES = 0x0300,

            KEY_READ = (StandardAccessRights.STANDARD_RIGHTS_READ | KEY_QUERY_VALUE | KEY_ENUMERATE_SUB_KEYS | KEY_NOTIFY) & ~StandardAccessRights.SYNCHRONIZE,
            KEY_WRITE = (StandardAccessRights.STANDARD_RIGHTS_WRITE | KEY_SET_VALUE | KEY_CREATE_SUB_KEY) & ~StandardAccessRights.SYNCHRONIZE,
            KEY_EXECUTE = KEY_READ & ~StandardAccessRights.SYNCHRONIZE,

            KEY_ALL_ACCESS = StandardAccessRights.STANDARD_RIGHTS_ALL | KEY_QUERY_VALUE | KEY_SET_VALUE | KEY_CREATE_SUB_KEY | KEY_ENUMERATE_SUB_KEYS | KEY_NOTIFY | KEY_CREATE_LINK & ~StandardAccessRights.SYNCHRONIZE,
        }

        internal enum DeviceInfoRegistryProperty : uint
        {
            //
            // Device registry property codes
            // (Codes marked as read-only (R) may only be used for
            // SetupDiGetDeviceRegistryProperty)
            //
            // These values should cover the same set of registry properties
            // as defined by the CM_DRP codes in cfgmgr32.h.
            //
            // Note that SPDRP codes are zero based while CM_DRP codes are one based!
            //
            SPDRP_DEVICEDESC = 0x00000000,  // DeviceDesc = R/W,
            SPDRP_HARDWAREID = 0x00000001,  // HardwareID = R/W,
            SPDRP_COMPATIBLEIDS = 0x00000002,  // CompatibleIDs = R/W,
            SPDRP_UNUSED0 = 0x00000003,  // unused
            SPDRP_SERVICE = 0x00000004,  // Service = R/W,
            SPDRP_UNUSED1 = 0x00000005,  // unused
            SPDRP_UNUSED2 = 0x00000006,  // unused
            SPDRP_CLASS = 0x00000007,  // Class = R--tied to ClassGUID,
            SPDRP_CLASSGUID = 0x00000008,  // ClassGUID = R/W,
            SPDRP_DRIVER = 0x00000009,  // Driver = R/W,
            SPDRP_CONFIGFLAGS = 0x0000000A,  // ConfigFlags = R/W,
            SPDRP_MFG = 0x0000000B,  // Mfg = R/W,
            SPDRP_FRIENDLYNAME = 0x0000000C,  // FriendlyName = R/W,
            SPDRP_LOCATION_INFORMATION = 0x0000000D,  // LocationInformation = R/W,
            SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,  // PhysicalDeviceObjectName = R,
            SPDRP_CAPABILITIES = 0x0000000F,  // Capabilities = R,
            SPDRP_UI_NUMBER = 0x00000010,  // UiNumber = R,
            SPDRP_UPPERFILTERS = 0x00000011,  // UpperFilters = R/W,
            SPDRP_LOWERFILTERS = 0x00000012,  // LowerFilters = R/W,
            SPDRP_BUSTYPEGUID = 0x00000013,  // BusTypeGUID = R,
            SPDRP_LEGACYBUSTYPE = 0x00000014,  // LegacyBusType = R,
            SPDRP_BUSNUMBER = 0x00000015,  // BusNumber = R,
            SPDRP_ENUMERATOR_NAME = 0x00000016,  // Enumerator Name = R,
            SPDRP_SECURITY = 0x00000017,  // Security = R/W, binary form,
            SPDRP_SECURITY_SDS = 0x00000018,  // Security = W, SDS form,
            SPDRP_DEVTYPE = 0x00000019,  // Device Type = R/W,
            SPDRP_EXCLUSIVE = 0x0000001A,  // Device is exclusive-access = R/W,
            SPDRP_CHARACTERISTICS = 0x0000001B,  // Device Characteristics = R/W,
            SPDRP_ADDRESS = 0x0000001C,  // Device Address = R,
            SPDRP_UI_NUMBER_DESC_FORMAT = 0X0000001D,  // UiNumberDescFormat = R/W,
            SPDRP_DEVICE_POWER_DATA = 0x0000001E,  // Device Power Data = R,
            SPDRP_REMOVAL_POLICY = 0x0000001F,  // Removal Policy = R,
            SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x00000020,  // Hardware Removal Policy = R,
            SPDRP_REMOVAL_POLICY_OVERRIDE = 0x00000021,  // Removal Policy Override = RW,
            SPDRP_INSTALL_STATE = 0x00000022,  // Device Install State = R,
            SPDRP_LOCATION_PATHS = 0x00000023,  // Device Location Paths = R,
            SPDRP_BASE_CONTAINERID = 0x00000024,  // Base ContainerID = R,

            SPDRP_MAXIMUM_PROPERTY = 0x00000025,  // Upper bound on ordinals                
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct DevInfoData
        {
            public uint CbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public UIntPtr Reserved;
        }


        [DllImport("SetupAPI.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr SetupDiGetClassDevs(ref Guid classGuid, [MarshalAs(UnmanagedType.LPTStr)] string enumerator, IntPtr hwndParent, DiGetClassFlags flags);

        [DllImport("SetupAPI.dll", SetLastError = true)]
        internal static extern bool SetupDiEnumDeviceInfo(IntPtr deviceInfoSet, uint memberIndex, ref DevInfoData deviceInfoData);

        [DllImport("SetupAPI.dll", SetLastError = true)]
        internal static extern IntPtr SetupDiOpenDevRegKey(IntPtr deviceInfoSet, ref DevInfoData deviceInfoData, DeviceInfoPropertyScope scope, uint hwProfile, DeviceInfoRegistryKeyType keyType, RegistrySpecificAccessRights samDesired);

        [DllImport("SetupAPI.dll", SetLastError = true)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(IntPtr deviceInfoSet, ref DevInfoData deviceInfoData, DeviceInfoRegistryProperty property, out uint propertyRegDataType, StringBuilder propertyBuffer, uint propertyBufferSize, out uint requiredSize);

        [DllImport("SetupAPI.dll", SetLastError = true)]
        internal static extern int SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);
    }
}
