namespace BizLayer.ITF_PDA
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;

    [WebServiceBinding(Name="ITF_PDASoap", Namespace="http://tempuri.org/"), DebuggerStepThrough, DesignerCategory("code")]
    public class ITF_PDA : SoapHttpClientProtocol
    {
        public ITF_PDA()
        {
            base.Url = "http://localhost/heli/ITF_PDA.asmx";
        }

        public IAsyncResult BeginHelloWorld(AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
        }

        public IAsyncResult Beginuf_changepwd(string is_userid, string is_oldpwd, string is_newpwd, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_changepwd", new object[] { is_userid, is_oldpwd, is_newpwd }, callback, asyncState);
        }

        public IAsyncResult Beginuf_chk_shop(string as_shop, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_chk_shop", new object[] { as_shop, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_chk_storetrans(U_WTO_MOVE au_wto_move, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_chk_storetrans", new object[] { au_wto_move, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_chk_stuid(string as_stuid, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_chk_stuid", new object[] { as_stuid, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_ConnTest(AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_ConnTest", new object[0], callback, asyncState);
        }

        public IAsyncResult Beginuf_GetCPO(string is_Pof, string is_PoID, string is_Date, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetCPO", new object[] { is_Pof, is_PoID, is_Date, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetGoodsIIO(string is_goods, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetGoodsIIO", new object[] { is_goods, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetGoodsWTO(string is_goods, string is_warehouse, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetGoodsWTO", new object[] { is_goods, is_warehouse, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetIIO(string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetIIO", new object[] { as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetIIODT(string is_iio, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetIIODT", new object[] { is_iio, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetIIOITEM(string is_iio, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetIIOITEM", new object[] { is_iio, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetLoginName(string userid, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetLoginName", new object[] { userid, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetMatStorQty(string is_plant, string is_material, string is_stgeloc, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetMatStorQty", new object[] { is_plant, is_material, is_stgeloc, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetMoveRsn(string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetMoveRsn", new object[] { as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetPO(string is_warehouse, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetPO", new object[] { is_warehouse, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetPOOR(string is_warehouse, string is_pono, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetPOOR", new object[] { is_warehouse, is_pono, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetPOPF(string is_proid, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetPOPF", new object[] { is_proid, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetRECORD(string is_Pof, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetRECORD", new object[] { is_Pof, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_getSrvcPath(AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_getSrvcPath", new object[0], callback, asyncState);
        }

        public IAsyncResult Beginuf_getVersion(AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_getVersion", new object[0], callback, asyncState);
        }

        public IAsyncResult Beginuf_GetWTO(string is_warehouse, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetWTO", new object[] { is_warehouse, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetWTOExtTime(string is_warehouse, DateTime idt_starttime, DateTime idt_endtime, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetWTOExtTime", new object[] { is_warehouse, idt_starttime, idt_endtime, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetWTOOR(string is_warehouse, string is_wto, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetWTOOR", new object[] { is_warehouse, is_wto, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_GetWTOPF(string is_proid, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_GetWTOPF", new object[] { is_proid, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_login(string is_userid, string is_userpwd, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_login", new object[] { is_userid, is_userpwd }, callback, asyncState);
        }

        public IAsyncResult Beginuf_pda_getpdacontrolpriview(string is_user_id, string is_formname, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_pda_getpdacontrolpriview", new object[] { is_user_id, is_formname, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_pda_getpdapriview(string is_user_id, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_pda_getpdapriview", new object[] { is_user_id, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_pda_getroleid(string is_user_id, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_pda_getroleid", new object[] { is_user_id, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_pda_getstoreroompriview(string is_user_id, string is_storeroom, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_pda_getstoreroompriview", new object[] { is_user_id, is_storeroom, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_pda_getworkshoppriview(string is_user_id, string is_workshop, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_pda_getworkshoppriview", new object[] { is_user_id, is_workshop, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_SetIIO(U_IIO au_iio, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_SetIIO", new object[] { au_iio, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_SetPO(U_PDA_PO[] au_pda_po, DataSet dsPda, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_SetPO", new object[] { au_pda_po, dsPda, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_SetStorTrans(U_WTO_MOVE au_wto_move, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_SetStorTrans", new object[] { au_wto_move, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_SetWTO(U_PDA_WTO[] au_pda_wto, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_SetWTO", new object[] { au_pda_wto, as_MsgError }, callback, asyncState);
        }

        public IAsyncResult Beginuf_UpdatePrint(int ii_Print, string is_Pof, string as_MsgError, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("uf_UpdatePrint", new object[] { ii_Print, is_Pof, as_MsgError }, callback, asyncState);
        }

        public string EndHelloWorld(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public string Enduf_changepwd(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public int Enduf_chk_shop(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        public int Enduf_chk_storetrans(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        public int Enduf_chk_stuid(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        public string Enduf_ConnTest(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public DataSet Enduf_GetCPO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetGoodsIIO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetGoodsWTO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetIIO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetIIODT(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetIIOITEM(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetLoginName(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public decimal Enduf_GetMatStorQty(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (decimal) objArray[0];
        }

        public DataSet Enduf_GetMoveRsn(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetPO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetPOOR(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetPOPF(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetRECORD(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public string Enduf_getSrvcPath(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public string Enduf_getVersion(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public DataSet Enduf_GetWTO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetWTOExtTime(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetWTOOR(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_GetWTOPF(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public string Enduf_login(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public DataSet Enduf_pda_getpdacontrolpriview(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_pda_getpdapriview(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public DataSet Enduf_pda_getroleid(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        public bool Enduf_pda_getstoreroompriview(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (bool) objArray[0];
        }

        public bool Enduf_pda_getworkshoppriview(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (bool) objArray[0];
        }

        public int Enduf_SetIIO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        public string Enduf_SetPO(IAsyncResult asyncResult, out DataSet dsPda, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            dsPda = (DataSet) objArray[1];
            as_MsgError = (string) objArray[2];
            return (string) objArray[0];
        }

        public bool Enduf_SetStorTrans(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (bool) objArray[0];
        }

        public int Enduf_SetWTO(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        public int Enduf_UpdatePrint(IAsyncResult asyncResult, out string as_MsgError)
        {
            object[] objArray = base.EndInvoke(asyncResult);
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string HelloWorld()
        {
            return (string) base.Invoke("HelloWorld", new object[0])[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_changepwd", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string uf_changepwd(string is_userid, string is_oldpwd, string is_newpwd)
        {
            return (string) base.Invoke("uf_changepwd", new object[] { is_userid, is_oldpwd, is_newpwd })[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_chk_shop", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public int uf_chk_shop(string as_shop, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_chk_shop", new object[] { as_shop, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_chk_storetrans", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public int uf_chk_storetrans(U_WTO_MOVE au_wto_move, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_chk_storetrans", new object[] { au_wto_move, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_chk_stuid", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public int uf_chk_stuid(string as_stuid, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_chk_stuid", new object[] { as_stuid, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_ConnTest", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string uf_ConnTest()
        {
            return (string) base.Invoke("uf_ConnTest", new object[0])[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetCPO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetCPO(string is_Pof, string is_PoID, string is_Date, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetCPO", new object[] { is_Pof, is_PoID, is_Date, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetGoodsIIO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetGoodsIIO(string is_goods, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetGoodsIIO", new object[] { is_goods, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetGoodsWTO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetGoodsWTO(string is_goods, string is_warehouse, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetGoodsWTO", new object[] { is_goods, is_warehouse, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetIIO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetIIO(ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetIIO", new object[] { as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetIIODT", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetIIODT(string is_iio, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetIIODT", new object[] { is_iio, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetIIOITEM", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetIIOITEM(string is_iio, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetIIOITEM", new object[] { is_iio, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetLoginName", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetLoginName(string userid, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetLoginName", new object[] { userid, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetMatStorQty", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public decimal uf_GetMatStorQty(string is_plant, string is_material, string is_stgeloc, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetMatStorQty", new object[] { is_plant, is_material, is_stgeloc, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (decimal) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetMoveRsn", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetMoveRsn(ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetMoveRsn", new object[] { as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetPO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetPO(string is_warehouse, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetPO", new object[] { is_warehouse, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetPOOR", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetPOOR(string is_warehouse, string is_pono, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetPOOR", new object[] { is_warehouse, is_pono, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetPOPF", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetPOPF(string is_proid, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetPOPF", new object[] { is_proid, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetRECORD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetRECORD(string is_Pof, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetRECORD", new object[] { is_Pof, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_getSrvcPath", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string uf_getSrvcPath()
        {
            return (string) base.Invoke("uf_getSrvcPath", new object[0])[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_getVersion", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string uf_getVersion()
        {
            return (string) base.Invoke("uf_getVersion", new object[0])[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetWTO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetWTO(string is_warehouse, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetWTO", new object[] { is_warehouse, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetWTOExtTime", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetWTOExtTime(string is_warehouse, DateTime idt_starttime, DateTime idt_endtime, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetWTOExtTime", new object[] { is_warehouse, idt_starttime, idt_endtime, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetWTOOR", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetWTOOR(string is_warehouse, string is_wto, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetWTOOR", new object[] { is_warehouse, is_wto, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_GetWTOPF", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_GetWTOPF(string is_proid, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_GetWTOPF", new object[] { is_proid, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_login", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string uf_login(string is_userid, string is_userpwd)
        {
            return (string) base.Invoke("uf_login", new object[] { is_userid, is_userpwd })[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_pda_getpdacontrolpriview", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_pda_getpdacontrolpriview(string is_user_id, string is_formname, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_pda_getpdacontrolpriview", new object[] { is_user_id, is_formname, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_pda_getpdapriview", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_pda_getpdapriview(string is_user_id, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_pda_getpdapriview", new object[] { is_user_id, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_pda_getroleid", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet uf_pda_getroleid(string is_user_id, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_pda_getroleid", new object[] { is_user_id, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (DataSet) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_pda_getstoreroompriview", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public bool uf_pda_getstoreroompriview(string is_user_id, string is_storeroom, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_pda_getstoreroompriview", new object[] { is_user_id, is_storeroom, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (bool) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_pda_getworkshoppriview", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public bool uf_pda_getworkshoppriview(string is_user_id, string is_workshop, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_pda_getworkshoppriview", new object[] { is_user_id, is_workshop, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (bool) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_SetIIO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public int uf_SetIIO(U_IIO au_iio, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_SetIIO", new object[] { au_iio, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_SetPO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string uf_SetPO(U_PDA_PO[] au_pda_po, ref DataSet dsPda, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_SetPO", new object[] { au_pda_po, dsPda, as_MsgError });
            dsPda = (DataSet) objArray[1];
            as_MsgError = (string) objArray[2];
            return (string) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_SetStorTrans", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public bool uf_SetStorTrans(U_WTO_MOVE au_wto_move, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_SetStorTrans", new object[] { au_wto_move, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (bool) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_SetWTO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public int uf_SetWTO(U_PDA_WTO[] au_pda_wto, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_SetWTO", new object[] { au_pda_wto, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }

        [SoapDocumentMethod("http://tempuri.org/uf_UpdatePrint", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public int uf_UpdatePrint(int ii_Print, string is_Pof, ref string as_MsgError)
        {
            object[] objArray = base.Invoke("uf_UpdatePrint", new object[] { ii_Print, is_Pof, as_MsgError });
            as_MsgError = (string) objArray[1];
            return (int) objArray[0];
        }
    }
}

