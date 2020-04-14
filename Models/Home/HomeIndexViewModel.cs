using online_shopping.DAL;
using online_shopping.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace online_shopping.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitofWork = new GenericUnitOfWork();
        online_shoppingEntities context = new online_shoppingEntities();

        public IPagedList<Tbl_Product> ListOfProducts { get; set; }

        public HomeIndexViewModel CreateModel(string search,int  pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@search", search??(object)DBNull.Value)
            };
            IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListOfProducts =data
                
                
               
            };
        }

    }
}