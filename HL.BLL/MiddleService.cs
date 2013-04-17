namespace BizLayer
{
    using BizLayer.ITF_PDA;
    using Entity;
    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Web.Services.Protocols;

    public class MiddleService
    {
        private BizLayer.ITF_PDA.ITF_PDA PDAservice = new BizLayer.ITF_PDA.ITF_PDA();

        public MiddleService()
        {
            this.PDAservice.Url = Management.GetSingleton().DefaultBaseUrl + "//ITF_PDA.asmx";
        }

        public void ChangePassword(string name, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(newPassword))
            {
                throw new ApplicationException("用户名或密码不能够为空！");
            }
            string message = "";
            try
            {
                message = this.PDAservice.uf_changepwd(name, oldPassword, newPassword);
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            if (message != "Success")
            {
                throw new ApplicationException(message);
            }
        }

        public int CheckPart(string partID, ref string MsgError)
        {
            int num = -1;
            try
            {
                num = this.PDAservice.uf_chk_shop(partID, ref MsgError);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (num != 0)
            {
                throw new ApplicationException(MsgError);
            }
            return num;
        }

        public int CheckStuid(string stuidID, ref string MsgError)
        {
            int num = -1;
            try
            {
                num = this.PDAservice.uf_chk_stuid(stuidID, ref MsgError);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (num != 0)
            {
                throw new ApplicationException(MsgError);
            }
            return num;
        }

        public void ConnectionTest(string url)
        {
            string str = "";
            try
            {
                this.PDAservice.Url = url + "//ITF_PDA.asmx";
                str = this.PDAservice.uf_ConnTest();
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (str != "OK")
            {
                throw new ApplicationException("测试连接失败！");
            }
        }

        public DataSet dsGetTaskByID(string proofid, EnumTaskType taskType)
        {
            DataSet set = new DataSet();
            string str = "";
            try
            {
                switch (taskType)
                {
                    case EnumTaskType.CheckIn:
                        return this.PDAservice.uf_GetPOPF(proofid, ref str);

                    case EnumTaskType.CheckOut:
                        return this.PDAservice.uf_GetWTOPF(proofid, ref str);
                }
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }

        /// <summary>
        /// 按订单收发货
        /// </summary>
        /// <param name="warehouse">仓库</param>
        /// <param name="taskorderid">订单号</param>
        /// <param name="taskType">任务类型</param>
        /// <returns></returns>
        public DataSet dsGetTaskByOrderID(string warehouse, string taskorderid, EnumTaskType taskType)
        {
            DataSet set = new DataSet();
            string str = "";
            try
            {
                switch (taskType)
                {
                    case EnumTaskType.CheckIn:
                        return this.PDAservice.uf_GetPOOR(warehouse, taskorderid, ref str);

                    case EnumTaskType.CheckOut:
                        return this.PDAservice.uf_GetWTOOR(warehouse, taskorderid, ref str);
                }
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }

        public string GetCabPath()
        {
            string str = "";
            try
            {
                str = this.PDAservice.uf_getSrvcPath();
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            return str;
        }

        public int GetCompletedTaskCount()
        {
            DirectoryInfo info = new DirectoryInfo(FilePath.CollectionResultPath);
            return info.GetFiles("*Y.txt").Length;
        }

        public DataSet GetGetCPO(string pof, string PoID, string date)
        {
            DataSet set = new DataSet();
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetCPO(pof, PoID, date, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }

        public DataSet GetGetWTOExtTime(string warehouseid, DateTime begintime, DateTime endtime)
        {
            DataSet set = new DataSet();
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetWTOExtTime(warehouseid, begintime, endtime, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }

        public DataSet GetGoodsCheckoutTasks(string goodsid, string warehouseid)
        {
            DataSet set;
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetGoodsWTO(goodsid, warehouseid, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (str != "OK")
            {
                throw new ApplicationException(str);
            }
            return set;
        }

        public DataSet GetGoodsInspectTasks(string goodsid)
        {
            DataSet set;
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetGoodsIIO(goodsid, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (str != "OK")
            {
                throw new ApplicationException(str);
            }
            return set;
        }

        public DataSet GetInspectItem(string taskid)
        {
            DataSet set = new DataSet();
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetIIOITEM(taskid, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }
        /// <summary>
        /// 获得全部检验数据。
        /// </summary>
        /// <returns></returns>
        public DataSet GetInspectTasks()
        {
            DataSet set;
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetIIO(ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (str != "OK")
            {
                throw new ApplicationException(str);
            }
            return set;
        }

        public DataSet GetInTasks(string id)
        {
            DataSet set;
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetPO(id, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }

        public decimal GetInventoryQty(string plant, string goodsid, string Location, ref string MsgExrror)
        {
            decimal num = 0M;
            try
            {
                num = this.PDAservice.uf_GetMatStorQty(plant, goodsid, Location, ref MsgExrror);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return num;
        }

        public DataSet GetLoginName(string userid)
        {
            DataSet set;
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetLoginName(userid, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (Exception exception)
            {
                throw new ApplicationException("未知错误", exception);
            }
            return set;
        }

        public DataSet GetMoveReason()
        {
            DataSet set;
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetMoveRsn(ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (Exception exception)
            {
                throw new ApplicationException("未知错误", exception);
            }
            return set;
        }

        public DataSet GetOutTasks(string id)
        {
            DataSet set;
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetWTO(id, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }

        public DataSet GetPDAPriview(string userid)
        {
            string str = string.Empty;
            DataSet set = new DataSet();
            try
            {
                set = this.PDAservice.uf_pda_getpdapriview(userid, ref str);
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            return set;
        }

        public DataSet GetRECORD(string pof)
        {
            DataSet set = new DataSet();
            string str = "";
            try
            {
                set = this.PDAservice.uf_GetRECORD(pof, ref str);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            return set;
        }

        public DataSet GetRoleID(string userid)
        {
            string str = string.Empty;
            DataSet set = new DataSet();
            try
            {
                set = this.PDAservice.uf_pda_getroleid(userid, ref str);
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            return set;
        }

        public string GetVersion()
        {
            string str = "";
            try
            {
                str = this.PDAservice.uf_getVersion();
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            return str;
        }

        public bool GetWarehouspriview(string userid, string warehouseid, ref string MsgError)
        {
            bool flag = false;
            try
            {
                flag = this.PDAservice.uf_pda_getstoreroompriview(userid, warehouseid, ref MsgError);
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            return flag;
        }

        public bool GetWorkshoppriview(string userid, string partid, ref string MsgError)
        {
            bool flag = false;
            try
            {
                flag = this.PDAservice.uf_pda_getworkshoppriview(userid, partid, ref MsgError);
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            return flag;
        }

        public void Login(string workCode, string password)
        {
            string message = "";
            try
            {
                message = this.PDAservice.uf_login(workCode, password);
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!", exception);
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            if (message != "Success")
            {
                throw new ApplicationException(message);
            }
        }

        public bool SetStorTrans(MoveGoods movegoods, ref string MsgExrror)
        {
            bool flag = false;
            try
            {
                U_WTO_MOVE u_wto_move = new U_WTO_MOVE();
                u_wto_move.Is_Matcode = movegoods.GoodsID;
                u_wto_move.Id_Qty = movegoods.Qty;
                u_wto_move.Is_StorFrom = movegoods.Fromwarehouse;
                u_wto_move.Is_StorTo = movegoods.Towarehouse;
                u_wto_move.Is_Factory = movegoods.Fromwarehouse.Substring(0, 4).ToString();
                u_wto_move.Is_Propoid = movegoods.Toorder;
                u_wto_move.Ii_srtype = 2;
                u_wto_move.Is_TransPersion = movegoods.Outtype;
                flag = this.PDAservice.uf_SetStorTrans(u_wto_move, ref MsgExrror);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (!flag)
            {
                throw new ApplicationException(MsgExrror);
            }
            return flag;
        }

        public string SubmitInRecord(string orderid, string proid, string supplyid, string loctionid, string userid, Task t, ref DataSet ListPda, ref string as_MsErr)
        {
            string str = string.Empty;
            ArrayList keys = t.InsList.Keys as ArrayList;
            U_PDA_PO[] u_pda_poArray = new U_PDA_PO[keys.Count];
            U_PDA_PO[] u_pda_poArray2 = new U_PDA_PO[keys.Count];
            int index = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                bool flag = false;
                int num3 = 0;
                U_PDA_PO u_pda_po = new U_PDA_PO();
                u_pda_po.Is_USER = userid;
                u_pda_po.Is_ClientID = supplyid;
                u_pda_po.Is_PROPOID = orderid;
                u_pda_po.Is_PROSTUID = loctionid;
                u_pda_po.Is_ProID = proid;
                U_PDA_PO_DT[] u_pda_po_dtArray = new U_PDA_PO_DT[(t.InsList[keys[i]] as Instruction).Result.ColDetails.Count];
                foreach (DictionaryEntry entry in (t.InsList[keys[i]] as Instruction).Result.ColDetails)
                {
                    flag = true;
                    ColDetail detail = entry.Value as ColDetail;
                    U_PDA_PO_DT u_pda_po_dt = new U_PDA_PO_DT();
                    u_pda_po_dt.ILs_TORSITE = detail.Location;
                    u_pda_po_dt.ILd_TORBACTHNO = detail.BatchNumber;
                    u_pda_po_dt.ILs_GDSID = detail.GoodsID;
                    u_pda_po_dt.ILdt_TORFDATE = detail.CollectedTime;
                    u_pda_po_dt.ILi_TORFINIQTY = detail.CollectedQuantity;
                    u_pda_po_dt.ILs_TORCUSTID = userid;
                    u_pda_po_dt.ILs_TIRUNIT = detail.Unit;

                    u_pda_po.Is_TIOPOLID = detail.Rowno;
                    u_pda_po_dtArray[num3] = u_pda_po_dt;
                    num3++;
                }
                if (flag)
                {
                    u_pda_po.IL_poDT = u_pda_po_dtArray;
                    u_pda_po.Is_TIOSNO = ((Instruction) t.InsList[keys[i]]).ID;
                    u_pda_poArray[index] = u_pda_po;
                    index++;
                }
            }
            try
            {
                if (index > 0)
                {
                    u_pda_poArray2 = new U_PDA_PO[index];
                    int num4 = 0;
                    for (int j = 0; j < u_pda_poArray.Length; j++)
                    {
                        if (u_pda_poArray[j] != null)
                        {
                            u_pda_poArray2[num4] = u_pda_poArray[j];
                            num4++;
                        }
                    }
                }
                str = this.PDAservice.uf_SetPO(u_pda_poArray2, ref ListPda, ref as_MsErr);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            return str;
        }

        public void SubmitInspectRecord(string userid, string[] arrTask, ref string as_MsErr)
        {
            U_IIO u_iio = new U_IIO();
            u_iio.Is_InspNo = arrTask[0].ToString();
            u_iio.Id_ReachQty = decimal.Parse(arrTask[4].ToString());
            if (arrTask[6].ToString() == "")
            {
                u_iio.Id_DefectQty = 0M;
            }
            else
            {
                u_iio.Id_DefectQty = decimal.Parse(arrTask[6].ToString());
            }
            u_iio.Ii_InspResult = Convert.ToInt32(arrTask[7]);
            u_iio.Id_ResultQty_OK = decimal.Parse(arrTask[8].ToString());
            u_iio.Id_ResultQty_NG = decimal.Parse(arrTask[9].ToString());
            u_iio.Is_ColDesc = arrTask[10].ToString();
            u_iio.Is_MoveRsn = arrTask[11].ToString();
            u_iio.Is_InspUser = userid;
            u_iio.Idt_InspDate = DateTime.Now;
            int num = 1;
            try
            {
                num = this.PDAservice.uf_SetIIO(u_iio, ref as_MsErr);
            }
            catch (WebException exception)
            {
                throw new ApplicationException("连接服务器失败!" + exception.Message.ToString());
            }
            catch (SoapException exception2)
            {
                throw new ApplicationException("服务器错误", exception2);
            }
            catch (Exception exception3)
            {
                throw new ApplicationException("未知错误", exception3);
            }
            if (num != 0)
            {
                throw new ApplicationException(as_MsErr);
            }
        }

        public void SubmitOutRecord(string orderid, string userid, Task t, ref string as_MsErr)
        {
            int num = -1;
            ArrayList keys = t.InsList.Keys as ArrayList;
            U_PDA_WTO[] u_pda_wtoArray = new U_PDA_WTO[keys.Count];
            U_PDA_WTO[] u_pda_wtoArray2 = new U_PDA_WTO[keys.Count];
            int index = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                bool flag = false;
                int num4 = 0;
                U_PDA_WTO u_pda_wto = new U_PDA_WTO();
                u_pda_wto.Is_PROPOID = orderid;
                U_PDA_WTO_DT[] u_pda_wto_dtArray = new U_PDA_WTO_DT[(t.InsList[keys[i]] as Instruction).Result.ColDetails.Count];
                foreach (DictionaryEntry entry in (t.InsList[keys[i]] as Instruction).Result.ColDetails)
                {
                    flag = true;
                    ColDetail detail = entry.Value as ColDetail;
                    U_PDA_WTO_DT u_pda_wto_dt = new U_PDA_WTO_DT();
                    u_pda_wto_dt.ILs_TORSITE = detail.Location;
                    u_pda_wto_dt.ILs_TORBACTHNO = detail.BatchNumber;
                    u_pda_wto_dt.ILdt_TORFDATE = detail.CollectedTime;
                    u_pda_wto_dt.ILi_TORFINIQTY = detail.CollectedQuantity;
                    u_pda_wto_dt.ILs_TORCUSTID = userid;
                    u_pda_wto_dtArray[num4] = u_pda_wto_dt;
                    num4++;
                }
                if (flag)
                {
                    u_pda_wto.IL_wtoDT = u_pda_wto_dtArray;
                    u_pda_wto.Is_TIOSNO = ((Instruction) t.InsList[keys[i]]).ID;
                    u_pda_wtoArray[index] = u_pda_wto;
                    index++;
                }
            }
            try
            {
                if (index > 0)
                {
                    u_pda_wtoArray2 = new U_PDA_WTO[index];
                    int num5 = 0;
                    for (int j = 0; j < u_pda_wtoArray.Length; j++)
                    {
                        if (u_pda_wtoArray[j] != null)
                        {
                            u_pda_wtoArray2[num5] = u_pda_wtoArray[j];
                            num5++;
                        }
                    }
                }
                num = this.PDAservice.uf_SetWTO(u_pda_wtoArray2, ref as_MsErr);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            if (num != 0)
            {
                throw new ApplicationException(as_MsErr);
            }
        }

        public int UpdatePrint(int times, string ProofID, ref string MsgError)
        {
            new DataSet();
            int num = -1;
            try
            {
                num = this.PDAservice.uf_UpdatePrint(times, ProofID, ref MsgError);
            }
            catch (WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (SoapException exception)
            {
                throw new ApplicationException("服务器错误", exception);
            }
            catch (Exception exception2)
            {
                throw new ApplicationException("未知错误", exception2);
            }
            if (num != 0)
            {
                throw new ApplicationException(MsgError);
            }
            return num;
        }

        public string ServiceUrl
        {
            get
            {
                return this.PDAservice.Url;
            }
            set
            {
                this.PDAservice.Url = value + "//ITF_PDA.asmx";
            }
        }
    }
}

