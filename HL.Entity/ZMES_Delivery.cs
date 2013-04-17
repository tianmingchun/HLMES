using System;

using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// PDA采购收货业务记录表
    /// </summary>
    public class ZMES_Delivery
    {
        /// <summary>
        /// 分组ID，GUID
        /// 一组有多个行项目，组ID相同。
        /// </summary>
        public string Delivery_ID;      
        /// <summary>
        /// 收货人
        /// </summary>
        public string UserName;     
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string EBELN;
        /// <summary>
        /// 行项目号
        /// </summary>
        public string EBELP;
        /// <summary>
        /// 收货数量
        /// </summary>
        public decimal WEMNG;
        /// <summary>
        /// 物料号
        /// </summary>
        public string MATNR;
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MAKTX;
        /// <summary>
        /// 采购订单数量
        /// </summary>
        public decimal MENGE;
        /// <summary>
        /// 单位
        /// </summary>
        public string MEINS;
        /// <summary>
        /// 交货工厂
        /// </summary>
        public string SWerks;
        /// <summary>
        /// 交货仓库
        /// </summary>
        public string LGOBE;
        /// <summary>
        /// 交货日期
        /// </summary>
        public string EINDT;
        /// <summary>
        /// 公司代码
        /// </summary>
        public string BUKRS;
        /// <summary>
        /// 采购凭证类型
        /// </summary>
        public string BSART;
        /// <summary>
        /// 采购组
        /// </summary>
        public string EKGRP;
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string LIFNR;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string LIFNRName;
        /// <summary>
        /// 发货工厂
        /// </summary>
        public string FWerks;
        /// <summary>
        /// 收货状态（“S”成功，“E”失败）
        /// </summary>
        public string Del_State;
        /// <summary>
        /// 返回备注（成功时返回物料凭证号，否则返回失败原因）
        /// </summary>
        public string Del_Remark;
        /// <summary>
        /// 收货日期
        /// </summary>
        public string Del_Date;
        /// <summary>
        /// 收货时间
        /// </summary>
        public string Del_Time;
        /// <summary>
        /// 同步状态（空表示“待同步”；“X”表示已同步
        /// </summary>
        public string Syn_State;

    }
}
