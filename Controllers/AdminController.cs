using online_shopping.DAL;
using online_shopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using online_shopping.Models;
using Newtonsoft.Json;

namespace online_shopping.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitofWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCatagory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitofWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach(var item in cat)
            {
                list.Add(new SelectListItem { Value = item.categoryID.ToString(), Text = item.categoryName });
            }
            return list;
        }
             
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _unitofWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i =>i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory() /* add category in admin */
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Tbl_Category Tbl) /* add category in admin */
        {
            _unitofWork.GetRepositoryInstance<Tbl_Category>().Add(Tbl);
            return RedirectToAction("Categories");
        }
      
        public ActionResult UpdateCategory(int categoryID)
        {
            CategoryDetail cd;
                if(categoryID != 0) 
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitofWork.GetRepositoryInstance<Tbl_Category>().GetFirstDefault(categoryID)));
            }
            else
            {
                cd = new CategoryDetail();

            }
            return View("UpdateCategory", cd); /*call view of update category */

        }
        public ActionResult CategoryEdit(int catId)
        {
           

            return View(_unitofWork.GetRepositoryInstance<Tbl_Category>().GetFirstDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {

            _unitofWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }
        public ActionResult Product()
        { 
            return View(_unitofWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }
        public ActionResult ProductEdit(int productID)
        {
            ViewBag.CategoryList = GetCatagory();

            return View(_unitofWork.GetRepositoryInstance<Tbl_Product>().GetFirstDefault(productID));
        }
        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
       {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);

                file.SaveAs(path);
            }
            tbl.productImg = file != null ? pic :tbl.productImg;
            tbl.modifiedDate = DateTime.Now;
            _unitofWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            return RedirectToAction("Product");
        }
        public ActionResult ProductADD()
        {
            ViewBag.CategoryList = GetCatagory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductADD(Tbl_Product tbl,HttpPostedFileBase file)
        {
            string pic= null;
            if (file != null)
            {
                 pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);

                file.SaveAs(path);
            }

            tbl.productImg = pic;
            tbl.createdDate = DateTime.Now;
            _unitofWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);
            return RedirectToAction("Product");
        }

        public ActionResult DisplayOrders()
        {
            online_shoppingEntities ctx = new online_shoppingEntities();
            return View(_unitofWork.GetRepositoryInstance<DisplayOrder>().GetProduct());
        }
    }
}