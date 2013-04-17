using System;

using System.Collections.Generic;
using System.Text;

namespace HL.UI
{
    public enum TaskType
    {
        /// <summary>
        /// 外部采购订单收货
        /// </summary>
        OuterOrderCheckIn,
        /// <summary>
        /// 工厂间采购订单收货
        /// </summary>
        FactoryOrderCheckIn,
        /// <summary>
        /// 生产订单收货
        /// </summary>
        ProductOrderCheckIn,
        /// <summary>
        /// 物殊收货
        /// </summary>
        SpecialCheckIn,
        /// <summary>
        /// 工厂间采购订单发货
        /// </summary>
        FactoryOrderCheckOut,
        /// <summary>
        /// 生产订单发货
        /// </summary>
        ProductOrderCheckOut,
        /// <summary>
        /// 预留单发货
        /// </summary>
        PreserveCheckOut,
        /// <summary>
        /// 特殊发货
        /// </summary>
        SpecialCheckOut

    }
}
