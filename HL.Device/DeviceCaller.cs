using System;

using System.Collections.Generic;
using System.Text;

namespace HL.Device
{
    //modi by tmc,2013-04-16
    public enum PdaDeviceType
    {
        HT5000 = 0,
        Catchwell = 1
    }

    /// <summary>
    /// PDA设备调用接口。
    /// 根据设备型号，调用不同的设备驱动DLL。
    /// </summary>
    public class DeviceCaller
    {
        public static PdaDeviceType DeviceType = PdaDeviceType.HT5000;

        /// <summary>
        /// 检查网络状态。
        /// </summary>
        /// <returns></returns>
        public static bool CheckNetworkStat()
        {
            if (DeviceCaller.DeviceType == PdaDeviceType.HT5000)
            {
                return true;
                //return LC.Device.CheckNetworkStat();
            }
            else
            {
                return false;//未完成。
            }
        }
    }
}
