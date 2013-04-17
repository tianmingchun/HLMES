namespace DLLImport
{
    using System;
    using System.Runtime.InteropServices;

    internal class DLL_Import
    {
        [DllImport("SharpEx.dll")]
        public static extern bool BluetoothIsAdapted();
        [DllImport("SharpEx.dll")]
        public static extern bool BluetoothPowerOff();
        [DllImport("SharpEx.dll")]
        public static extern bool BluetoothPowerOn();
        [DllImport("SharpEx.dll")]
        public static extern bool BluetoothPowerStstus();
        [DllImport("SharpEx.dll", SetLastError=true)]
        public static extern bool ClearLastResult();
        [DllImport("SharpEx.dll")]
        public static extern bool DevicePowerOff();
        [DllImport("SharpEx.dll")]
        public static extern bool DeviceReboot();
        [DllImport("SharpEx.dll")]
        public static extern bool DeviceSleepOff();
        [DllImport("SharpEx.dll")]
        public static extern bool DeviceSleepOn();
        [DllImport("SharpEx.dll")]
        public static extern int GetBacklightBrightness();
        [DllImport("SharpEx.dll")]
        public static extern int GetBackLightBrightness();
        [DllImport("SharpEx.dll")]
        public static extern int GetBacklightOffTime();
        [DllImport("SharpEx.dll")]
        public static extern int GetBackLightOffTime();
        [DllImport("SharpEx.dll")]
        public static extern bool GetDeviceSerial(char[] Serial);
        [DllImport("SharpEx.dll")]
        public static extern bool GetDllVersion(char[] Verison);
        [DllImport("SharpEx.dll")]
        public static extern bool GetExtInStatus();
        [DllImport("SharpEx.dll", SetLastError=true)]
        public static extern bool GetLastBarcode(char[] Buffer);
        [DllImport("SharpEx.dll", SetLastError=true)]
        public static extern bool GetLastBarTypeStr(char[] Buffer);
        [DllImport("SharpEx.dll")]
        public static extern bool GetModemInformation(ModemInformation[] ModemInfo);
        [DllImport("SharpEx.dll")]
        public static extern bool GetModemInformationlp(char[] Manufacturer, char[] Model, char[] Revision, char[] IMEI, char[] IMSI);
        [DllImport("SharpEx.dll")]
        public static extern bool GetMotionSensorValue();
        [DllImport("SharpEx.dll")]
        public static extern void GetSharpExVersion(char[] Buffer);
        [DllImport("SharpEx.dll")]
        public static extern int GetUsbConnection();
        [DllImport("SharpEx.dll")]
        public static extern int GetUsbHostMode();
        [DllImport("SharpEx.dll")]
        public static extern int GetVolumeSize();
        [DllImport("SharpEx.dll")]
        public static extern bool GpsPowerOff();
        [DllImport("SharpEx.dll")]
        public static extern bool GpsPowerOn();
        [DllImport("SharpEx.dll")]
        public static extern bool GpsPowerStatus();
        [DllImport("SharpEx.dll")]
        public static extern bool KeypadBacklightOff();
        [DllImport("SharpEx.dll")]
        public static extern bool KeypadBacklightOn();
        [DllImport("SharpEx.dll")]
        public static extern bool KeypadBacklightOnTimer(int Milliseconds);
        [DllImport("SharpEx.dll")]
        public static extern bool MotionSensorPowerOff();
        [DllImport("SharpEx.dll")]
        public static extern bool MotionSensorPowerOn();
        [DllImport("SharpEx.dll")]
        public static extern bool MotionSensorPowerStatus();
        [DllImport("SharpEx.dll")]
        public static extern bool MotionSensorValue(AXIValue[] AXIValue);
        [DllImport("SharpEx.dll")]
        public static extern bool PreviewStart(int Height, int Width, int X, int Y);
        [DllImport("SharpEx.dll")]
        public static extern bool PreviewStop();
        [DllImport("SharpEx.dll")]
        public static extern int ReadMotionSensorValueAccX();
        [DllImport("SharpEx.dll")]
        public static extern int ReadMotionSensorValueAccY();
        [DllImport("SharpEx.dll")]
        public static extern int ReadMotionSensorValueAccZ();
        [DllImport("SharpEx.dll")]
        public static extern int ReadMotionSensorValueAzimuth();
        [DllImport("SharpEx.dll")]
        public static extern int ReadMotionSensorValueMagX();
        [DllImport("SharpEx.dll")]
        public static extern int ReadMotionSensorValueMagY();
        [DllImport("SharpEx.dll")]
        public static extern int ReadMotionSensorValueMagZ();
        [DllImport("SharpEx.dll")]
        public static extern bool SetBacklightBrightness(int Brightness);
        [DllImport("SharpEx.dll")]
        public static extern bool SetBackLightBrightness(int Brightness);
        [DllImport("SharpEx.dll")]
        public static extern bool SetBacklightOffTime(int Milliseconds);
        [DllImport("SharpEx.dll")]
        public static extern bool SetBackLightOffTime(int Time);
        [DllImport("SharpEx.dll")]
        public static extern bool SetBarcode(int iType, bool bEnable);
        [DllImport("SharpEx.dll")]
        public static extern bool SetBeamHold(bool IsHold);
        [DllImport("SharpEx.dll")]
        public static extern bool SetCaputreMode(int iType);
        [DllImport("SharpEx.dll")]
        public static extern bool SetGunMode(bool bEnable);
        [DllImport("SharpEx.dll")]
        public static extern bool SetMessageWindow(IntPtr hWnd);
        [DllImport("SharpEx.dll")]
        public static extern bool SetOpMode(int iOpMode);
        [DllImport("SharpEx.dll")]
        public static extern bool SetPictureBoxWnd(IntPtr hWnd);
        [DllImport("SharpEx.dll")]
        public static extern bool SetScanningLightMode(bool bEnable);
        [DllImport("SharpEx.dll")]
        public static extern bool SetShowImage(bool bShow);
        [DllImport("SharpEx.dll")]
        public static extern bool SetSimpleScanMode(bool bEnable);
        [DllImport("SharpEx.dll")]
        public static extern bool SetUsbConnection(int iMode);
        [DllImport("SharpEx.dll")]
        public static extern bool SetUsbHostMode(int iMode);
        [DllImport("SharpEx.dll")]
        public static extern bool SetVolumeSize(int Volume);
        [DllImport("SharpEx.dll")]
        public static extern void TrigKeyDownEmu();
        [DllImport("SharpEx.dll")]
        public static extern void TrigKeyUpEmu();
        [DllImport("SharpEx.dll")]
        public static extern void VibratorPowerOff();
        [DllImport("SharpEx.dll")]
        public static extern void VibratorPowerOn();
        [DllImport("SharpEx.dll")]
        public static extern bool VibratorPowerOnTimer(int Milliseconds);
        [DllImport("SharpEx.dll")]
        public static extern bool VibratorPowerStatus();
        [DllImport("SharpEx.dll")]
        public static extern bool WLANIsAdapted();
        [DllImport("SharpEx.dll")]
        public static extern bool WLANPowerOff();
        [DllImport("SharpEx.dll")]
        public static extern bool WLANPowerOn();
        [DllImport("SharpEx.dll")]
        public static extern bool WLANPowerStatus();
        [DllImport("SharpEx.dll")]
        public static extern bool WWANIsAdapted();
        [DllImport("SharpEx.dll")]
        public static extern bool WWANPowerOff();
        [DllImport("SharpEx.dll")]
        public static extern bool WWANPowerOn();
        [DllImport("SharpEx.dll")]
        public static extern bool WWANPowerStstus();
    }
}

