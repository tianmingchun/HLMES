namespace BizLayer
{
    using System;
    using System.IO;

    public class FilePath
    {
        public static string CollectionResultPath
        {
            get
            {
                string path = Path.Combine(WMSPath, "results");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        public static string RegulationFilePath
        {
            get
            {
                return Path.Combine(RegulationPath, "regulation.xml");
            }
        }

        public static string RegulationPath
        {
            get
            {
                string path = Path.Combine(WMSPath, "regulation");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        public static string TasksPath
        {
            get
            {
                string path = Path.Combine(WMSPath, "tasks");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        public static string UserInfoFilePath
        {
            get
            {
                return Path.Combine(UserInfoPath, "users.xml");
            }
        }

        public static string UserInfoPath
        {
            get
            {
                string path = Path.Combine(WMSPath, "userInfo");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        public static string WMSPath
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.WinCE)
                {
                    if (Management.GetSingleton().Scanner == "HHP")
                    {
                        if (!Directory.Exists(@"\IPSM\WMSWireless"))
                        {
                            Directory.CreateDirectory(@"\IPSM\WMSWireless");
                        }
                        return @"\IPSM\WMSWireless";
                    }
                    if (Management.GetSingleton().Scanner == "Honeywell")
                    {
                        if (!Directory.Exists(@"\Honeywell\WMSWireless"))
                        {
                            Directory.CreateDirectory(@"\Honeywell\WMSWireless");
                        }
                        return @"\Honeywell\WMSWireless";
                    }
                }
                return "";
            }
        }
    }
}

