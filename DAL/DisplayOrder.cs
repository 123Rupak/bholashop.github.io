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
    
    public partial class DisplayOrder
    {
        public int memberID { get; set; }
        public string UserName { get; set; }
        public int order_p_id { get; set; }
        public string order_product { get; set; }
        public Nullable<int> order_qty { get; set; }
        public Nullable<decimal> order_price { get; set; }
        public int order_id { get; set; }
        public string order_name { get; set; }
        public Nullable<decimal> order_phone_no { get; set; }
        public string order_add { get; set; }
        public Nullable<System.DateTime> date_time { get; set; }
    }
}
