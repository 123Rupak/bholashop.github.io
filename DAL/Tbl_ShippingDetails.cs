//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace online_shopping.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_ShippingDetails
    {
        public int shippingDetailsID { get; set; }
        public Nullable<int> memberID { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public Nullable<int> orderId { get; set; }
        public Nullable<decimal> amountPaid { get; set; }
        public string paymentType { get; set; }
    
        public virtual Tbl_Members Tbl_Members { get; set; }
    }
}
