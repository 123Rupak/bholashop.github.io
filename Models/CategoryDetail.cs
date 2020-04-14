using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq; 
using System.Web;
using System.Web.Mvc;

namespace online_shopping.Models
{
    public class CategoryDetail
    {
        public int categoryId { get; set; }
        [Required (ErrorMessage ="Category Name Required")]
        [StringLength(100,ErrorMessage ="Minimum 3 and minimum 5 and maximum 100 character are allowed",MinimumLength =3)]
        public string categoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
    public class PeoductDetail
    {

         public int productID { get; set; }
        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 character are allowed",MinimumLength =3)]
        public string productName { get; set; }
        [Required]
        [Range(1,50)]
        public Nullable<int> categoryID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        [Required(ErrorMessage ="Description is Requirede")]
        public Nullable<System.DateTime> description { get; set; }
        public string productImg { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        [Required]
        [Range(typeof(int),"1","500",ErrorMessage ="Invalid Quantity")]
        public Nullable<int> quantity { get; set; }
        [Required]
        [Range(typeof(decimal),"1","2000000",ErrorMessage ="Invalid Price")]
        public Nullable<decimal> price { get; set; }
        





    }


}