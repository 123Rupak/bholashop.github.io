﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class online_shoppingEntities : DbContext
    {
        public online_shoppingEntities()
            : base("name=online_shoppingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Cart> Tbl_Cart { get; set; }
        public virtual DbSet<Tbl_CartStatus> Tbl_CartStatus { get; set; }
        public virtual DbSet<Tbl_Category> Tbl_Category { get; set; }
        public virtual DbSet<Tbl_Roles> Tbl_Roles { get; set; }
        public virtual DbSet<Tbl_ShippingDetails> Tbl_ShippingDetails { get; set; }
        public virtual DbSet<Tbl_SlideImage> Tbl_SlideImage { get; set; }
        public virtual DbSet<Tbl_Product> Tbl_Product { get; set; }
        public virtual DbSet<Tbl_OrdererDetails> Tbl_OrdererDetails { get; set; }
        public virtual DbSet<Tbl_OrderProDetails> Tbl_OrderProDetails { get; set; }
        public virtual DbSet<Tbl_MemberRole> Tbl_MemberRole { get; set; }
        public virtual DbSet<Tbl_Members> Tbl_Members { get; set; }
        public virtual DbSet<Tbl_UserAccount> Tbl_UserAccount { get; set; }
        public virtual DbSet<DisplayOrder> DisplayOrders { get; set; }
        public virtual DbSet<Tbl_AdminLogin> Tbl_AdminLogin { get; set; }
    
        public virtual ObjectResult<displyOrder_Result> displyOrder()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<displyOrder_Result>("displyOrder");
        }
    
        public virtual ObjectResult<Tbl_Members> Fun_DisplayOrder()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tbl_Members>("Fun_DisplayOrder");
        }
    
        public virtual ObjectResult<Tbl_Members> Fun_DisplayOrder(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tbl_Members>("Fun_DisplayOrder", mergeOption);
        }
    }
}
