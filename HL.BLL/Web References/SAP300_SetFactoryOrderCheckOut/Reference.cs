﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3643
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.CompactFramework.Design.Data 2.0.50727.3643 版自动生成。
// 
namespace HL.BLL.SAP300_SetFactoryOrderCheckOut {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    // CODEGEN: 未处理命名空间“http://schemas.xmlsoap.org/ws/2004/09/policy”中的可选 WSDL 扩展元素“Policy”。
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="zmes_factorycaigoufahuo", Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class zmes_factorycaigoufahuo : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public zmes_factorycaigoufahuo() {
            this.Url = "http://r3dev01.heli.com:8000/sap/bc/srt/rfc/sap/zmes_factorycaigoufahuo/300/zmes_" +
                "factorycaigoufahuo/zmes_factorycaigoufahuo";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sap-com:document:sap:rfc:functions:ZMES_FACTORYCAIGOUFAHUO:ZMES_FACTORYCAIGOU" +
            "FAHUORequest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ZMES_FACTORYCAIGOUFAHUOResponse", Namespace="urn:sap-com:document:sap:rfc:functions")]
        public ZMES_FACTORYCAIGOUFAHUOResponse ZMES_FACTORYCAIGOUFAHUO([System.Xml.Serialization.XmlElementAttribute("ZMES_FACTORYCAIGOUFAHUO", Namespace="urn:sap-com:document:sap:rfc:functions")] ZMES_FACTORYCAIGOUFAHUO ZMES_FACTORYCAIGOUFAHUO1) {
            object[] results = this.Invoke("ZMES_FACTORYCAIGOUFAHUO", new object[] {
                        ZMES_FACTORYCAIGOUFAHUO1});
            return ((ZMES_FACTORYCAIGOUFAHUOResponse)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginZMES_FACTORYCAIGOUFAHUO(ZMES_FACTORYCAIGOUFAHUO ZMES_FACTORYCAIGOUFAHUO1, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ZMES_FACTORYCAIGOUFAHUO", new object[] {
                        ZMES_FACTORYCAIGOUFAHUO1}, callback, asyncState);
        }
        
        /// <remarks/>
        public ZMES_FACTORYCAIGOUFAHUOResponse EndZMES_FACTORYCAIGOUFAHUO(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ZMES_FACTORYCAIGOUFAHUOResponse)(results[0]));
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZMES_FACTORYCAIGOUFAHUO {
        
        private ZMES_SENDRECORD[] tB_SENDRECORDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZMES_SENDRECORD[] TB_SENDRECORD {
            get {
                return this.tB_SENDRECORDField;
            }
            set {
                this.tB_SENDRECORDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZMES_SENDRECORD {
        
        private string mANDTField;
        
        private string dELIVERY_IDField;
        
        private string uSERNAMEField;
        
        private string dEL_DATEField;
        
        private string dEL_TIMEField;
        
        private string fLGOBEField;
        
        private string eBELNField;
        
        private string eBELPField;
        
        private decimal wAMNGField;
        
        private string mATNRField;
        
        private string mAKTXField;
        
        private decimal mENGEField;
        
        private string mEINSField;
        
        private string sWERKSField;
        
        private string sLGOBEField;
        
        private string eINDTField;
        
        private string bUKRSField;
        
        private string bSARTField;
        
        private string eKGRPField;
        
        private string lIFNRField;
        
        private string lIFNRNAMEField;
        
        private string fWERKSField;
        
        private string sTATEField;
        
        private string rEMARKField;
        
        private string bUD_DATEField;
        
        private string bUD_TIMEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MANDT {
            get {
                return this.mANDTField;
            }
            set {
                this.mANDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DELIVERY_ID {
            get {
                return this.dELIVERY_IDField;
            }
            set {
                this.dELIVERY_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string USERNAME {
            get {
                return this.uSERNAMEField;
            }
            set {
                this.uSERNAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DEL_DATE {
            get {
                return this.dEL_DATEField;
            }
            set {
                this.dEL_DATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DEL_TIME {
            get {
                return this.dEL_TIMEField;
            }
            set {
                this.dEL_TIMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FLGOBE {
            get {
                return this.fLGOBEField;
            }
            set {
                this.fLGOBEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EBELN {
            get {
                return this.eBELNField;
            }
            set {
                this.eBELNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EBELP {
            get {
                return this.eBELPField;
            }
            set {
                this.eBELPField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal WAMNG {
            get {
                return this.wAMNGField;
            }
            set {
                this.wAMNGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATNR {
            get {
                return this.mATNRField;
            }
            set {
                this.mATNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MAKTX {
            get {
                return this.mAKTXField;
            }
            set {
                this.mAKTXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal MENGE {
            get {
                return this.mENGEField;
            }
            set {
                this.mENGEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MEINS {
            get {
                return this.mEINSField;
            }
            set {
                this.mEINSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SWERKS {
            get {
                return this.sWERKSField;
            }
            set {
                this.sWERKSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SLGOBE {
            get {
                return this.sLGOBEField;
            }
            set {
                this.sLGOBEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EINDT {
            get {
                return this.eINDTField;
            }
            set {
                this.eINDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BUKRS {
            get {
                return this.bUKRSField;
            }
            set {
                this.bUKRSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BSART {
            get {
                return this.bSARTField;
            }
            set {
                this.bSARTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EKGRP {
            get {
                return this.eKGRPField;
            }
            set {
                this.eKGRPField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LIFNR {
            get {
                return this.lIFNRField;
            }
            set {
                this.lIFNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LIFNRNAME {
            get {
                return this.lIFNRNAMEField;
            }
            set {
                this.lIFNRNAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FWERKS {
            get {
                return this.fWERKSField;
            }
            set {
                this.fWERKSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STATE {
            get {
                return this.sTATEField;
            }
            set {
                this.sTATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string REMARK {
            get {
                return this.rEMARKField;
            }
            set {
                this.rEMARKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BUD_DATE {
            get {
                return this.bUD_DATEField;
            }
            set {
                this.bUD_DATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BUD_TIME {
            get {
                return this.bUD_TIMEField;
            }
            set {
                this.bUD_TIMEField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZMES_FACTORYCAIGOUFAHUOResponse {
        
        private string rETMSGField;
        
        private string rETVALField;
        
        private ZMES_SENDRECORD[] tB_SENDRECORDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RETMSG {
            get {
                return this.rETMSGField;
            }
            set {
                this.rETMSGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RETVAL {
            get {
                return this.rETVALField;
            }
            set {
                this.rETVALField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZMES_SENDRECORD[] TB_SENDRECORD {
            get {
                return this.tB_SENDRECORDField;
            }
            set {
                this.tB_SENDRECORDField = value;
            }
        }
    }
}
