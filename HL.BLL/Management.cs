namespace BizLayer
{
    using Entity;
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Xml;

    public class Management
    {
        private string inLocation = "";
        private string invLocation = "";
        private static Management management;
        private string mode = "";
        private bool needProduceBatchNo;
        private string outLocation = "";
        private string partNo = "";
        private RegulationCheck Regulation = RegulationCheck.GetInstance();
        private string scanner = "";
        private DateTime stamp = DateTime.MinValue;
        private string url = "";
        private string warehouseNo = "";
        private string serviceUserName = "";
        private string servicePassword = "";

        private Management()
        {
            this.GetConfig();
        }

        public bool CheckCommonBatchID(string content)
        {
            return this.Regulation.Check(content, RegulationCheck.DetailFields.BatchNo);
        }

        public bool CheckCommonLocation(string content)
        {
            return this.Regulation.Check(content, RegulationCheck.DetailFields.Location);
        }

        public bool CheckQuantity(string count)
        {
            Regex regex = new Regex(@"^\d+(\.\d{1,3})?$");
            if (!regex.IsMatch(count))
            {
                return false;
            }
            return true;
        }

        public void GetConfig()
        {
            XmlDocument document = new XmlDocument();
            string codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
            codeBase = codeBase.Substring(0, codeBase.LastIndexOf(@"\") + 1);
            if (!File.Exists(Path.Combine(codeBase, "App.cfg")))
            {
                throw new ApplicationException("找不到配置文件！");
            }
            try
            {
                document.Load(Path.Combine(codeBase, "App.cfg"));
            }
            catch (XmlException)
            {
                throw new ApplicationException("配置文件错误！");
            }
            XmlNodeList childNodes = document.DocumentElement.ChildNodes[0].ChildNodes;
            for (int i = 0; i < childNodes.Count; i++)
            {
                if (childNodes[i] is XmlElement)
                {
                    XmlNode node = childNodes[i];
                    if (node.Attributes["key"].InnerText == "ServerUrl")
                    {
                        this.url = node.Attributes["value"].InnerText;
                    }
                    else if (node.Attributes["key"].InnerText == "ScanMode")
                    {
                        if (!(node.Attributes["value"].InnerText == "1") && !(node.Attributes["value"].InnerText == "2"))
                        {
                            throw new ApplicationException("采集模式设置错误！");
                        }
                        this.mode = node.Attributes["value"].InnerText;
                    }
                    else if (node.Attributes["key"].InnerText == "Scanner")
                    {
                        this.scanner = node.Attributes["value"].InnerText;
                    }
                    else
                    {
                        if (node.Attributes["key"].InnerText == "Stamp")
                        {
                            try
                            {
                                this.stamp = Convert.ToDateTime(node.Attributes["value"].InnerText);
                                goto Label_04E2;
                            }
                            catch (FormatException)
                            {
                                throw new ApplicationException("非法的时间戳！");
                            }
                        }
                        if (node.Attributes["key"].InnerText == "InLocation")
                        {
                            if (!(node.Attributes["value"].InnerText == "1") && !(node.Attributes["value"].InnerText == "2"))
                            {
                                throw new ApplicationException("入库库位扫描模式错误！");
                            }
                            this.inLocation = node.Attributes["value"].InnerText;
                        }
                        else if (node.Attributes["key"].InnerText == "OutLocation")
                        {
                            if (!(node.Attributes["value"].InnerText == "1") && !(node.Attributes["value"].InnerText == "2"))
                            {
                                throw new ApplicationException("出库库位扫描模式错误！");
                            }
                            this.outLocation = node.Attributes["value"].InnerText;
                        }
                        else if (node.Attributes["key"].InnerText == "InvLocation")
                        {
                            if (!(node.Attributes["value"].InnerText == "1") && !(node.Attributes["value"].InnerText == "2"))
                            {
                                throw new ApplicationException("盘库位扫描模式错误！");
                            }
                            this.invLocation = node.Attributes["value"].InnerText;
                        }
                        else if (node.Attributes["key"].InnerText == "NeedProduceNo")
                        {
                            if (!(node.Attributes["value"].InnerText == "1") && !(node.Attributes["value"].InnerText == "0"))
                            {
                                throw new ApplicationException("设置是否需要采集生产批次错误！");
                            }
                            this.needProduceBatchNo = node.Attributes["value"].InnerText == "1";
                        }
                        else if (node.Attributes["key"].InnerText == "WarehouseNo")
                        {
                            this.warehouseNo = node.Attributes["value"].InnerText;
                        }
                        else if (node.Attributes["key"].InnerText == "PartNo")
                        {
                            this.partNo = node.Attributes["value"].InnerText;
                        }
                        else if (node.Attributes["key"].InnerText == "ServiceUserName")
                        {
                            this.serviceUserName = node.Attributes["value"].InnerText;
                        }
                        else if (node.Attributes["key"].InnerText == "ServicePassword")
                        {
                            this.servicePassword = node.Attributes["value"].InnerText;
                        }
                         
                    Label_04E2:;
                    }
                }
            }
        }

        public static Management GetSingleton()
        {
            object obj2 = new object();
            if (management == null)
            {
                lock (obj2)
                {
                    if (management == null)
                    {
                        management = new Management();
                    }
                }
            }
            return management;
        }

        private void LoadCommonGloableRegulation()
        {
            RegulationCheck instance = RegulationCheck.GetInstance();
            instance.GoodsCode.Clear();
            string str = "^*$";
            GoodsCodeRegex regex = new GoodsCodeRegex("01");
            regex.Regex = str;
            instance.GoodsCode.Add("01", regex);
            instance.GoodsIDRegex = new string[] { str };
            instance.LocationRegex = new string[] { str };
            instance.BatchNoRegex = new string[] { str };
        }

        public void LoadGloableRegulation()
        {
            RegulationCheck instance = RegulationCheck.GetInstance();
            instance.GoodsCode.Clear();
            if (!File.Exists(FilePath.RegulationFilePath))
            {
                this.LoadCommonGloableRegulation();
            }
            else
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(FilePath.RegulationFilePath);
                }
                catch (Exception exception)
                {
                    throw new ApplicationException("错误的规则文件" + exception.Message);
                }
                XmlNode documentElement = document.DocumentElement;
                for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                {
                    int num3;
                    XmlNode node2 = documentElement.ChildNodes[i];
                    if (!(node2.Name != "GoodsCodeRegexs"))
                    {
                        goto Label_0168;
                    }
                    ArrayList list = new ArrayList();
                    for (int j = 0; j < node2.ChildNodes.Count; j++)
                    {
                        list.Add(node2.ChildNodes[j].InnerText);
                    }
                    string name = node2.Name;
                    if (name == null)
                    {
                        goto Label_015D;
                    }
                    if (!(name == "GoodsIDRegexs"))
                    {
                        if (name == "LocationRegexs")
                        {
                            goto Label_011B;
                        }
                        if (name == "BatchNoRegexs")
                        {
                            goto Label_013C;
                        }
                        goto Label_015D;
                    }
                    instance.GoodsIDRegex = (string[]) list.ToArray(typeof(string));
                    goto Label_02B4;
                Label_011B:
                    instance.LocationRegex = (string[]) list.ToArray(typeof(string));
                    goto Label_02B4;
                Label_013C:
                    instance.BatchNoRegex = (string[]) list.ToArray(typeof(string));
                    goto Label_02B4;
                Label_015D:
                    throw new ApplicationException("错误的全局规则文件");
                Label_0168:
                    num3 = 0;
                    while (num3 < node2.ChildNodes.Count)
                    {
                        XmlNode node3 = node2.ChildNodes[num3];
                        GoodsCodeRegex regex = new GoodsCodeRegex(node3.Attributes["id"].InnerText);
                        regex.Regex = node3.Attributes["regex"].InnerText;
                        ArrayList list2 = new ArrayList();
                        for (int k = 0; k < node3.ChildNodes.Count; k++)
                        {
                            XmlNode node4 = node3.ChildNodes[k];
                            ElementRegex regex2 = new ElementRegex();
                            regex2.Name = node4.Attributes["name"].InnerText;
                            regex2.Start = Convert.ToInt32(node4.Attributes["start"].InnerText);
                            regex2.End = Convert.ToInt32(node4.Attributes["end"].InnerText);
                            list2.Add(regex2);
                        }
                        regex.Elements = (ElementRegex[]) list2.ToArray(typeof(ElementRegex));
                        instance.GoodsCode.Add(regex.ID, regex);
                        num3++;
                    }
                Label_02B4:;
                }
            }
        }

        public Task LoadRecord(Task t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("任务不能为空！");
            }
            string path = "";
            switch (t.TaskType)
            {
                case EnumTaskType.CheckIn:
                    path = Path.Combine(FilePath.CollectionResultPath, "IN" + t.ID + "$N.txt");
                    if (!File.Exists(path))
                    {
                        path = Path.Combine(FilePath.CollectionResultPath, "IN" + t.ID + "$Y.txt");
                    }
                    break;

                case EnumTaskType.CheckOut:
                    path = Path.Combine(FilePath.CollectionResultPath, "OUT" + t.ID + "$N.txt");
                    if (!File.Exists(path))
                    {
                        path = Path.Combine(FilePath.CollectionResultPath, "OUT" + t.ID + "$Y.txt");
                    }
                    break;

                default:
                    throw new ApplicationException("未知的任务类型！");
            }
            if (File.Exists(path))
            {
                ArrayList keys = t.InsList.Keys as ArrayList;
                for (int i = 0; i < keys.Count; i++)
                {
                    Instruction instruction = t.InsList[keys[i]] as Instruction;
                    using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                    {
                        while (reader.Peek() > 0)
                        {
                            string[] strArray = reader.ReadLine().Split(new char[] { '\t' });
                            if (strArray[0] == instruction.ID)
                            {
                                ColDetail detail;
                                if (t.TaskType == EnumTaskType.CheckIn)
                                {
                                    detail = new CheckInColDetail();
                                    ((CheckInColDetail) detail).ProduceBatchNo = strArray[7];
                                }
                                else
                                {
                                    detail = new ColDetail();
                                }
                                detail.Location = strArray[1];
                                detail.GoodsID = strArray[2];
                                detail.BatchNumber = strArray[3];
                                detail.SingletonNo = strArray[4];
                                detail.CollectedQuantity = Convert.ToInt32(strArray[5]);
                                detail.CollectedTime = Convert.ToDateTime(strArray[6]);
                                instruction.Result.AddColDetail(detail);
                            }
                        }
                    }
                }
                Thread.Sleep(100);
                File.Delete(path);
            }
            return t;
        }

        public int LocalLogin(string userid, string password)
        {
            if (!File.Exists(FilePath.UserInfoFilePath))
            {
                return -1;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(FilePath.UserInfoFilePath);
            }
            catch (Exception exception)
            {
                throw new ApplicationException("错误的用户信息文件" + exception.Message);
            }
            XmlNode node2 = document.DocumentElement.SelectSingleNode("/Users/User[@name='" + userid + "']");
            if (node2 == null)
            {
                return -1;
            }
            if (node2.Attributes["password"].Value == password)
            {
                return 1;
            }
            return 0;
        }

        public bool RegexCheck(string regex, string barcode)
        {
            Regex regex2 = new Regex(regex);
            if (regex.StartsWith("^"))
            {
                return regex2.IsMatch(barcode);
            }
            return (regex2.Match(barcode).Length == barcode.Length);
        }

        public void SaveConfig(object content, EnumSaveContent contentType)
        {
            XmlDocument document = new XmlDocument();
            string codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
            codeBase = codeBase.Substring(0, codeBase.LastIndexOf(@"\") + 1);
            if (!File.Exists(Path.Combine(codeBase, "App.cfg")))
            {
                throw new ApplicationException("找不到配置文件！");
            }
            try
            {
                document.Load(Path.Combine(codeBase, "App.cfg"));
            }
            catch (XmlException)
            {
                throw new ApplicationException("配置文件错误！");
            }
            foreach (XmlNode node in document.DocumentElement.ChildNodes[0].ChildNodes)
            {
                if (node is XmlElement)
                {
                    if (contentType == EnumSaveContent.Url)
                    {
                        if (!(node.Attributes["key"].InnerText == "ServerUrl"))
                        {
                            continue;
                        }
                        node.Attributes["value"].InnerText = content.ToString();
                        this.url = content.ToString();
                        break;
                    }
                    if (contentType == EnumSaveContent.Stamp)
                    {
                        if (!(node.Attributes["key"].InnerText == "Stamp"))
                        {
                            continue;
                        }
                        DateTime time = (DateTime) content;
                        node.Attributes["value"].InnerText = time.ToString("G");
                        this.stamp = time;
                        break;
                    }
                    if (contentType == EnumSaveContent.InLocation)
                    {
                        if (!(node.Attributes["key"].InnerText == "InLocation"))
                        {
                            continue;
                        }
                        node.Attributes["value"].InnerText = content.ToString();
                        this.inLocation = content.ToString();
                        break;
                    }
                    if (contentType == EnumSaveContent.OutLocation)
                    {
                        if (!(node.Attributes["key"].InnerText == "OutLocation"))
                        {
                            continue;
                        }
                        node.Attributes["value"].InnerText = content.ToString();
                        this.outLocation = content.ToString();
                        break;
                    }
                    if (contentType == EnumSaveContent.InvLocation)
                    {
                        if (!(node.Attributes["key"].InnerText == "InvLocation"))
                        {
                            continue;
                        }
                        node.Attributes["value"].InnerText = content.ToString();
                        this.invLocation = content.ToString();
                        break;
                    }
                    if (contentType == EnumSaveContent.Warehouse)
                    {
                        if (!(node.Attributes["key"].InnerText == "WarehouseNo"))
                        {
                            continue;
                        }
                        node.Attributes["value"].InnerText = content.ToString();
                        this.warehouseNo = content.ToString();
                        break;
                    }
                    if ((contentType == EnumSaveContent.Part) && (node.Attributes["key"].InnerText == "PartNo"))
                    {
                        node.Attributes["value"].InnerText = content.ToString();
                        this.partNo = content.ToString();
                        break;
                    }
                }
            }
            document.Save(Path.Combine(codeBase, "App.cfg"));
        }

        public void SaveGolableRegulation()
        {
            XmlTextWriter writer = new XmlTextWriter(FilePath.RegulationFilePath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Regulation");
            writer.WriteStartElement("GoodsIDRegexs");
            for (int i = 0; i < this.Regulation.GoodsIDRegex.Length; i++)
            {
                writer.WriteElementString("Regex", this.Regulation.GoodsIDRegex[i]);
            }
            writer.WriteEndElement();
            writer.WriteStartElement("LocationRegexs");
            for (int j = 0; j < this.Regulation.LocationRegex.Length; j++)
            {
                writer.WriteElementString("Regex", this.Regulation.LocationRegex[j]);
            }
            writer.WriteEndElement();
            writer.WriteStartElement("BatchNoRegexs");
            for (int k = 0; k < this.Regulation.BatchNoRegex.Length; k++)
            {
                writer.WriteElementString("Regex", this.Regulation.BatchNoRegex[k]);
            }
            writer.WriteEndElement();
            writer.WriteStartElement("GoodsCodeRegexs");
            foreach (DictionaryEntry entry in this.Regulation.GoodsCode)
            {
                GoodsCodeRegex regex = entry.Value as GoodsCodeRegex;
                writer.WriteStartElement("Regulation");
                writer.WriteAttributeString("id", regex.ID);
                writer.WriteAttributeString("regex", regex.Regex);
                if (regex.Elements != null)
                {
                    for (int m = 0; m < regex.Elements.Length; m++)
                    {
                        ElementRegex regex2 = regex.Elements[m];
                        writer.WriteStartElement("element");
                        writer.WriteAttributeString("name", regex2.Name);
                        writer.WriteAttributeString("start", regex2.Start.ToString());
                        writer.WriteAttributeString("end", regex2.End.ToString());
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public void SaveLocationCfg()
        {
            this.SaveConfig(this.inLocation, EnumSaveContent.InLocation);
            this.SaveConfig(this.outLocation, EnumSaveContent.OutLocation);
            this.SaveConfig(this.invLocation, EnumSaveContent.InvLocation);
        }

        public void SaveRecord(Task t, bool complete)
        {
            if (t == null)
            {
                throw new ArgumentNullException("采集记录不能够为空!");
            }
            if (t.TotalQuantity != 0M)
            {
                string filePath = "";
                switch (t.TaskType)
                {
                    case EnumTaskType.CheckIn:
                        if (!complete)
                        {
                            filePath = Path.Combine(FilePath.CollectionResultPath, "IN" + t.ID + "$N.txt");
                            break;
                        }
                        filePath = Path.Combine(FilePath.CollectionResultPath, "IN" + t.ID + "$Y.txt");
                        break;

                    case EnumTaskType.CheckOut:
                        if (!complete)
                        {
                            filePath = Path.Combine(FilePath.CollectionResultPath, "OUT" + t.ID + "$N.txt");
                            break;
                        }
                        filePath = Path.Combine(FilePath.CollectionResultPath, "OUT" + t.ID + "$Y.txt");
                        break;

                    default:
                        throw new ApplicationException("未知的任务类型！");
                }
                WriteTask(t, filePath);
            }
        }

        public void UpdateUserInfo(string userid, string password)
        {
            if (!File.Exists(FilePath.UserInfoFilePath))
            {
                XmlTextWriter writer = new XmlTextWriter(FilePath.UserInfoFilePath, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Users");
                writer.WriteStartElement("User");
                writer.WriteAttributeString("name", userid);
                writer.WriteAttributeString("password", password);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
            else
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(FilePath.UserInfoFilePath);
                }
                catch (Exception exception)
                {
                    throw new ApplicationException("错误的用户信息文件" + exception.Message);
                }
                XmlNode documentElement = document.DocumentElement;
                XmlNode node2 = documentElement.SelectSingleNode("/Users/User[@name='" + userid + "']");
                if (node2 == null)
                {
                    XmlElement newChild = document.CreateElement("User");
                    XmlAttribute node = document.CreateAttribute("name");
                    node.Value = userid;
                    XmlAttribute attribute2 = document.CreateAttribute("password");
                    attribute2.Value = password;
                    newChild.Attributes.Append(node);
                    newChild.Attributes.Append(attribute2);
                    documentElement.AppendChild(newChild);
                    document.Save(FilePath.UserInfoFilePath);
                }
                else
                {
                    node2.Attributes["password"].Value = password;
                    document.Save(FilePath.UserInfoFilePath);
                }
            }
        }

        private static void WriteTask(Task t, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                ArrayList keys = t.InsList.Keys as ArrayList;
                for (int i = 0; i < keys.Count; i++)
                {
                    Instruction instruction = t.InsList[keys[i]] as Instruction;
                    foreach (DictionaryEntry entry in instruction.Result.ColDetails)
                    {
                        char ch = '\t';
                        ColDetail detail = entry.Value as ColDetail;
                        StringBuilder builder = new StringBuilder(instruction.ID);
                        builder.Append(ch);
                        builder.Append(detail.Location);
                        builder.Append(ch);
                        builder.Append(detail.GoodsID);
                        builder.Append(ch);
                        builder.Append(detail.BatchNumber);
                        builder.Append(ch);
                        builder.Append(detail.SingletonNo);
                        builder.Append(ch);
                        builder.Append(detail.CollectedQuantity.ToString());
                        builder.Append(ch);
                        builder.Append(detail.CollectedTime.ToShortTimeString());
                        if (t.TaskType == EnumTaskType.CheckIn)
                        {
                            builder.Append(ch);
                            builder.Append(((CheckInColDetail) detail).ProduceBatchNo);
                        }
                        writer.WriteLine(builder.ToString());
                    }
                }
            }
        }

        public string DefaultBaseUrl
        {
            get
            {
                return this.url;
            }
        }

        public string InLocation
        {
            get
            {
                return this.inLocation;
            }
            set
            {
                this.inLocation = value;
            }
        }

        public string InvLocation
        {
            get
            {
                return this.invLocation;
            }
            set
            {
                this.invLocation = value;
            }
        }

        public bool NeedProduceBatchNo
        {
            get
            {
                return this.needProduceBatchNo;
            }
        }

        public string OutLocation
        {
            get
            {
                return this.outLocation;
            }
            set
            {
                this.outLocation = value;
            }
        }

        public string PartNo
        {
            get
            {
                return this.partNo;
            }
            set
            {
                this.partNo = value;
            }
        }

        public string ScanMode
        {
            get
            {
                return this.mode;
            }
        }

        public string Scanner
        {
            get
            {
                return this.scanner;
            }
        }

        public DateTime Stamp
        {
            get
            {
                return this.stamp;
            }
        }

        public string WarehouseNo
        {
            get
            {
                return this.warehouseNo;
            }
            set
            {
                this.warehouseNo = value;
            }
        }

        public string ServiceUserName
        {
            get
            {
                return this.serviceUserName;
            }
            set
            {
                this.serviceUserName = value;
            }
        }

        public string ServicePassword
        {
            get
            {
                return this.servicePassword;
            }
            set
            {
                this.servicePassword = value;
            }
        }
    }
}

