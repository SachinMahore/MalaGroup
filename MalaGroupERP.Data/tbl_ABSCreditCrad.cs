//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MalaGroupERP.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ABSCreditCrad
    {
        public long ID { get; set; }
        public string OpportunityID { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentStartDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string ChargeAmountAfterFirstPayment { get; set; }
        public string TransactionTotal { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string TransactionCount { get; set; }
        public string PaymentCount { get; set; }
        public string ChargeDate { get; set; }
        public string PasswordId { get; set; }
        public string PinNumber { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipPostal { get; set; }
        public string BillingCountry { get; set; }
        public string PrimaryPhone { get; set; }
        public string PersonAccountEmail { get; set; }
        public string PersonAccountFirstName { get; set; }
        public string PersonAccountLastName { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingStateProvince { get; set; }
        public string ShippingZipPostalCode { get; set; }
        public string ShippingCountry { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public string ChargeAmount { get; set; }
        public string TransactionID { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionMessage { get; set; }
        public string TransactionDate { get; set; }
        public string AuthCode { get; set; }
        public Nullable<int> IsSend { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedById { get; set; }
    }
}