using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MalaGroupERP.Models
{
    public class CardModel
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CardCode { get; set; }
        public decimal Amount { get; set; }
        public string OrderID { get; set; }
        public string TransactionID { get; set; }
        public string AuthCode { get; set; }
        public AgentOrderModel AOModel { get; set; }
        public string CustomerProfileID { get; set; }
        public string CustomerPaymentProfileID { get; set; }
        public string CustomerAddressID { get; set; }
        public string SubscriptionID { get; set; }
    }
}