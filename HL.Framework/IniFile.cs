namespace HL.Framework
{
    using System;
    using System.IO;
    using System.Text;

    public class IniFile
    {
        protected string IniFileName;

        public IniFile(string FileName)
        {
            this.IniFileName = FileName;
        }

        public void CreatInifile()
        {
            if (!Directory.Exists(this.IniFileName))
            {
                new FileStream(this.IniFileName, FileMode.Create, FileAccess.Write).Close();
            }
        }

        public bool ExistINIFile()
        {
            return File.Exists(this.IniFileName);
        }

        private string GetPrivateProfileString(string ApplicationName, string KeyName, string Default, string FileName)
        {
            string str = string.Format("[{0}]", ApplicationName);
            string str2 = KeyName + "=";
            StreamReader reader = null;
            try
            {
                string str3 = string.Empty;
                bool flag = false;
                reader = new StreamReader(FileName, Encoding.Default);
                str3 = reader.ReadLine();
                for (str3 = (str3 == null) ? string.Empty : str3.Trim(); str3.Length > 0; str3 = (str3 == null) ? string.Empty : str3.Trim())
                {
                    if (!flag)
                    {
                        if (str3 == str)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        if (str3.StartsWith("[") && str3.EndsWith("]"))
                        {
                            return Default;
                        }
                        if (str3.StartsWith(str2))
                        {
                            return str3.Substring(KeyName.Length + 1);
                        }
                    }
                    str3 = reader.ReadLine();
                }
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return Default;
        }

        public string ReadValue(string Section, string Key, string Default)
        {
            return this.GetPrivateProfileString(Section, Key, Default, this.IniFileName);
        }

        private void WritePrivateProfileString(string ApplicationName, string KeyName, string Value, string FileName)
        {
            string str = string.Format("[{0}]", ApplicationName);
            string str2 = KeyName + "=";
            try
            {
                StringBuilder builder = new StringBuilder();
                bool flag = false;
                bool flag2 = false;
                bool flag3 = true;
                StreamReader reader = new StreamReader(FileName, Encoding.Default);
                string str3 = reader.ReadLine();
                for (str3 = (str3 == null) ? string.Empty : str3.Trim(); str3.Length > 0; str3 = (str3 == null) ? string.Empty : str3.Trim())
                {
                    if (flag3)
                    {
                        if (!flag2)
                        {
                            builder.Append((builder.Length == 0) ? string.Empty : "\r\n");
                            builder.Append(str3);
                            if (str3 == str)
                            {
                                flag2 = true;
                            }
                        }
                        else if (str3.StartsWith("[") && str3.EndsWith("]"))
                        {
                            builder.Append(string.Format("\r\n{0}={1}", KeyName, Value));
                            builder.Append(string.Format("\r\n{0}", str3));
                            flag = true;
                            flag3 = false;
                        }
                        else if (str3.StartsWith(str2))
                        {
                            builder.Append(string.Format("\r\n{0}={1}", KeyName, Value));
                            flag = true;
                            flag3 = false;
                        }
                        else
                        {
                            builder.Append(string.Format("\r\n{0}", str3));
                        }
                    }
                    else
                    {
                        builder.Append(string.Format("\r\n{0}", str3));
                    }
                    str3 = reader.ReadLine();
                }
                reader.Close();
                if (!flag2)
                {
                    builder.Append((builder.Length == 0) ? string.Empty : "\r\n");
                    builder.Append(string.Format("[{0}]", ApplicationName));
                }
                if (!flag)
                {
                    builder.Append(string.Format("\r\n{0}={1}", KeyName, Value));
                }
                StreamWriter writer = new StreamWriter(FileName, false, Encoding.Default);
                writer.Write(builder);
                writer.Close();
            }
            catch
            {
            }
        }

        public void WriteValue(string Section, string Key, string Value)
        {
            this.WritePrivateProfileString(Section, Key, Value, this.IniFileName);
        }

        public string FileName
        {
            set
            {
                this.IniFileName = value;
            }
        }
    }
}

