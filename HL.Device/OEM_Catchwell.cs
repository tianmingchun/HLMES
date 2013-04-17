namespace SharpExCS
{
    using DLLImport;
    using System;

   
    /// <summary>
    /// Catctwell Interface
    /// dd
    /// 0000
    /// </summary>
    public class OEM_Catchwell
    {
     
        public static bool BluetoothIsAdapted()
        {
            //return DLL_Import.BluetoothIsAdapted();
            return false;
        }

        public static bool BluetoothPowerOff()
        {
            //return DLL_Import.BluetoothPowerOff();
            return false;
        }

        public static bool BluetoothPowerOn()
        {
            //return DLL_Import.BluetoothPowerOn();
            return false;
        }

        public static bool BluetoothPowerStstus()
        {
            //return DLL_Import.BluetoothPowerStstus();
            return false;
        }

        public static bool ClearLastResult()
        {
            //return DLL_Import.ClearLastResult();
            return false;
        }

        public static bool DevicePowerOff()
        {
            //return DLL_Import.DevicePowerOff();
            return false;
        }

        public static bool DeviceReboot()
        {
           // return DLL_Import.DeviceReboot();
            return false;
        }

        public static bool DeviceSleepOff()
        {
            //return DLL_Import.DeviceSleepOff();
            return false;
        }

        public static bool DeviceSleepOn()
        {
            //return DLL_Import.DeviceSleepOn();
            return false;
        }

        public static int GetBacklightBrightness()
        {
            //return DLL_Import.GetBacklightBrightness();
            return 0;
        }

        public static int GetBackLightBrightness()
        {
            //return DLL_Import.GetBackLightBrightness();
            return 0;
        }

        public static int GetBacklightOffTime()
        {
            //return DLL_Import.GetBacklightOffTime();
            return 0;
        }

        public static int GetBackLightOffTime()
        {
            //return DLL_Import.GetBackLightOffTime();
            return 0;
        }

        public static bool GetDeviceSerial(char[] Serial)
        {
            //return DLL_Import.GetDeviceSerial(Serial);
            return false;
        }

        public static bool GetDllVersion(char[] Verison)
        {
            //return DLL_Import.GetDllVersion(Verison);
            return false;
        }

        public static bool GetExtInStatus()
        {
            //return DLL_Import.GetExtInStatus();
            return false;
        }

        public static bool GetLastBarcode(char[] Buffer)
        {
            //return DLL_Import.GetLastBarcode(Buffer);
            return false;
        }

        public static bool GetLastBarcodeStr(char[] Buffer)
        {
            //return DLL_Import.GetLastBarTypeStr(Buffer);
            return false;
        }

        public static bool GetModemInformation(ModemInformation[] ModemInfo)
        {
            //return DLL_Import.GetModemInformation(ModemInfo);
            return false;
        }

        public static bool GetModemInformationlp(ref char[] Manufacturer, ref char[] Model, ref char[] Revision, ref char[] IMEI, ref char[] IMSI)
        {
            //return DLL_Import.GetModemInformationlp(Manufacturer, Model, Revision, IMEI, IMSI);
            return false;
        }

        public static bool GetMotionSensorValue()
        {
            //return DLL_Import.GetMotionSensorValue();
            return false;
        }

        public static void GetSharpExVersion(char[] Buffer)
        {
           // DLL_Import.GetSharpExVersion(Buffer);
           
        }

        public static int GetUsbConnection()
        {
            //return DLL_Import.GetUsbConnection();
            return 0;
        }

        public static int GetUsbHostMode()
        {
            //return DLL_Import.GetUsbHostMode();
            return 0;
        }

        public static int GetVolumeSize()
        {
            //return DLL_Import.GetVolumeSize();
            return 0;
        }

        public static bool GpsPowerOff()
        {
            //return DLL_Import.GpsPowerOff();
            return false;
        }

        public static bool GpsPowerOn()
        {
            //return DLL_Import.GpsPowerOn();
            return false;
        }

        public static bool GpsPowerStatus()
        {
            //return DLL_Import.GpsPowerStatus();
            return false;
        }

        public static bool KeypadBacklightOff()
        {
            //return DLL_Import.KeypadBacklightOff();
            return false;
        }

        public static bool KeypadBacklightOn()
        {
           // return DLL_Import.KeypadBacklightOn();
            return false;
        }

        public static bool KeypadBacklightOnTimer(int Milliseconds)
        {
            //return DLL_Import.KeypadBacklightOnTimer(Milliseconds);
            return false;
        }

        public static bool MotionSensorPowerOff()
        {
            //return DLL_Import.MotionSensorPowerOff();
            return false;
        }

        public static bool MotionSensorPowerOn()
        {
            //return DLL_Import.MotionSensorPowerOn();
            return false;
        }

        public static bool MotionSensorPowerStatus()
        {
            //return DLL_Import.MotionSensorPowerStatus();
            return false;
        }

        public static bool MotionSensorValue(AXIValue[] AXIValue)
        {
            //return DLL_Import.MotionSensorValue(AXIValue);
            return false;
        }

        public static bool PreviewStart(int Height, int Width, int X, int Y)
        {
            //return DLL_Import.PreviewStart(Height, Width, X, Y);
            return false;
        }

        public static bool PreviewStop()
        {
            //return DLL_Import.PreviewStop();
            return false;
        }

        public static int ReadMotionSensorValueAccX()
        {
            return DLL_Import.ReadMotionSensorValueAccX();
        }

        public static int ReadMotionSensorValueAccY()
        {
            //return DLL_Import.ReadMotionSensorValueAccY();
            return 0;
        }

        public static int ReadMotionSensorValueAccZ()
        {
            //return DLL_Import.ReadMotionSensorValueAccZ();
            return 0;
        }

        public static int ReadMotionSensorValueAzimuth()
        {
            //return DLL_Import.ReadMotionSensorValueAzimuth();
            return 0;
        }

        public static int ReadMotionSensorValueMagX()
        {
            return DLL_Import.ReadMotionSensorValueMagX();
        }

        public static int ReadMotionSensorValueMagY()
        {
            //return DLL_Import.ReadMotionSensorValueMagY();
            return 0;
        }

        public static int ReadMotionSensorValueMagZ()
        {
            //return DLL_Import.ReadMotionSensorValueMagZ();
            return 0;
        }

        public static bool SetBacklightBrightness(int Brightness)
        {
            //return DLL_Import.SetBacklightBrightness(Brightness);
            return false;
        }

        public static bool SetBackLightBrightness(int Brightness)
        {
            //return DLL_Import.SetBackLightBrightness(Brightness);
            return false;
        }

        public static bool SetBacklightOffTime(int Milliseconds)
        {
            //return DLL_Import.SetBacklightOffTime(Milliseconds);
            return false;
        }

        public static bool SetBackLightOffTime(int Time)
        {
            //return DLL_Import.SetBackLightOffTime(Time);
            return false;
        }

        public static bool SetBarcode(int iType, bool bEnable)
        {
            //return DLL_Import.SetBarcode(iType, bEnable);
            return false;
        }

        public static bool SetBeamHold(bool IsHold)
        {
            //return DLL_Import.SetBeamHold(IsHold);
            return false;
        }

        public static bool SetCaputreMode(int iType)
        {
            //return DLL_Import.SetCaputreMode(iType);
            return false;
        }

        public static bool SetGunMode(bool bEnable)
        {
            //return DLL_Import.SetGunMode(bEnable);
            return false;
        }

        public static bool SetMessageWindow(IntPtr hWnd)
        {
            //return DLL_Import.SetMessageWindow(hWnd);
            return false;
        }

        public static bool SetOpMode(int iOpMode)
        {
            //return DLL_Import.SetOpMode(iOpMode);
            return false;
        }

        public static bool SetPictureBoxWnd(IntPtr hWnd)
        {
            //return DLL_Import.SetPictureBoxWnd(hWnd);
            return false;
        }

        public static bool SetScanningLightMode(bool bEnable)
        {
            //return DLL_Import.SetScanningLightMode(bEnable);
            return false;
        }

        public static bool SetShowImage(bool bShow)
        {
            //return DLL_Import.SetShowImage(bShow);
            return false;
        }

        public static bool SetSimpleScanMode(bool bEnable)
        {
            //return DLL_Import.SetSimpleScanMode(bEnable);
            return false;
        }

        public static bool SetUsbConnection(int iMode)
        {
            //return DLL_Import.SetUsbConnection(iMode);
            return false;
        }

        public static bool SetUsbHostMode(int iMode)
        {
            //return DLL_Import.SetUsbHostMode(iMode);
            return false;
        }

        public static bool SetVolumeSize(int Volume)
        {
            //return DLL_Import.SetVolumeSize(Volume);
            return false;
        }

        public static void TrigKeyDownEmu()
        {
            //DLL_Import.TrigKeyDownEmu();
        }

        public static void TrigKeyUpEmu()
        {
            //DLL_Import.TrigKeyUpEmu();
        }

        public static void VibratorPowerOff()
        {
           // DLL_Import.VibratorPowerOff();
        }

        public static void VibratorPowerOn()
        {
            //DLL_Import.VibratorPowerOn();
        }

        public static bool VibratorPowerOnTimer(int Milliseconds)
        {
            //return DLL_Import.VibratorPowerOnTimer(Milliseconds);
            return false;
        }

        public static bool VibratorPowerStatus()
        {
            //return DLL_Import.VibratorPowerStatus();
            return false;
        }

        public static bool WLANIsAdapted()
        {
            //return DLL_Import.WLANIsAdapted();
            return false;
        }

        public static bool WLANPowerOff()
        {
            //return DLL_Import.WLANPowerOff();
            return false;
        }

        public static bool WLANPowerOn()
        {
            //return DLL_Import.WLANPowerOn();
            return false;
        }

        public static bool WLANPowerStatus()
        {
            //return DLL_Import.WLANPowerStatus();
            return false;
        }

        public static bool WWANIsAdapted()
        {
            //return DLL_Import.WWANIsAdapted();
            return false;
        }

        public static bool WWANPowerOff()
        {
            //return DLL_Import.WWANPowerOff();
            return false;
        }

        public static bool WWANPowerOn()
        {
            //return DLL_Import.WWANPowerOn();
            return false;
        }

        public static bool WWANPowerStstus()
        {
            //return DLL_Import.WWANPowerStstus();
            return false;
        }
    }
}

