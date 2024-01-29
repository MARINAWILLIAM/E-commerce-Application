using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        pending,//0
        [EnumMember(Value = "PaymentReceived")]
        paymentReceived,//1
        [EnumMember(Value = "PaymentFailed")]
        paymentFailed//2
    }
}
