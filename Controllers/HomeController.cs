using online_shopping.DAL;
using online_shopping.Home;
using online_shopping.Models.Home;
using online_shopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace online_shopping.Controllers
{
    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitofWork = new GenericUnitOfWork();
        online_shoppingEntities ctx = new online_shoppingEntities();


        public ActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Register(Tbl_Members member)
        {
         
            try
            {
                if (ModelState.IsValid)
                {
                   
                    //////////////check username//////
                    if (ctx.Tbl_Members.Any(x => x.UserName == member.UserName))
                    {
                        ViewBag.DuplicateMessage = "User Name Already Exist";
                        ModelState.Clear();
                        return View("Register");
                    }
                    else
                    {

                        member.createdOn = DateTime.Now;
                        _unitofWork.GetRepositoryInstance<Tbl_Members>().Add(member);

                        try
                        {
                            var sender = new MailAddress("demoact264@gmail.com", "Demo Test");
                            var receiver = new MailAddress(member.emailID, "Receiver");

                            var password = "9836241667";
                            var subject = "Confirmation Email";
                            var body = "Hi I am Rupak Das Welcome To Online Shopping THANKS FOR YOUR REGISTRATION";

                            var smtp = new SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(sender.Address, password)
                            };
                            using (var message = new MailMessage(sender, receiver)
                            {
                                Subject = subject,
                                Body = body
                            })
                            {
                                smtp.Send(message);
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        TempData["Msg"] = "You Have Registered succesfully";

                        ModelState.Clear();
                        return RedirectToAction("Register");
                       
                    }
                }
                return View(); 
                    
               
            }

            catch (Exception ex)
            {

                TempData["Msg"] = "There Must Be Some Problem ! Please Try Again Later'"+ex;
                return RedirectToAction("Register");


            }
        }
        public ActionResult AdminLogin()
        {
            if(Session["UserName"] != null)
            {
                return RedirectToAction("Dashboard", "Admin", new { });
            }
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Tbl_AdminLogin ADL)
        {
            var user = ctx.Tbl_AdminLogin.Where(a => a.Admin_UserName == ADL.Admin_UserName && a.Admin_Password == ADL.Admin_Password).FirstOrDefault();
            if(user != null)
            {
                Session["AdminId"] = user.Admin_id;
                Session["UserName"] = ADL.Admin_UserName;
                return RedirectToAction("Dashboard", "Admin", new { UserName = ADL.Admin_UserName});
            }
            else
            {
                return View();
            }
           
        }
        public ActionResult AdminLogout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Login()
            {

            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "Home", new { fistName = Session["UserName"].ToString() });
            }
            else
            {
                return View();
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Tbl_Members objM)
        {
            var user = ctx.Tbl_Members.Where(x => x.firstName == objM.firstName && x.password == objM.password ).FirstOrDefault();
         
            if (user != null)
            {

                Session["memberID"] = user.memberID;
                Session["UserName"] = objM.firstName;
                
                return RedirectToAction("Index", "Home", new { fistName = objM.firstName });
            }
            else
            {
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Index(string search, int? page)
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                HomeIndexViewModel model = new HomeIndexViewModel();
                return View(model.CreateModel(search, 4, page));

            }
        }

        public ActionResult Checkout()
        {
            return View();

        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.productID == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }


                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        public ActionResult AddToCart(int ProductId, string url)//add product in cart list 
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(ProductId);
                cart.Add(new Item()
                {

                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(ProductId);

                for (int i = 0; i < count; i++)
                {

                    if (cart[i].Product.productID == ProductId)
                    {

                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()


                        {

                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {

                        var prd = cart.Where(x => x.Product.productID == ProductId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }

                    }

                }
                Session["cart"] = cart;

            }
            return Redirect(url);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RemoveFromCart(int ProductId)//remove product from cart list
        {

            List<Item> cart = (List<Item>)Session["cart"];
            //   var product = ctx.Tbl_Product.Find(ProductId);
            foreach (var item in cart)
            {
                if (item.Product.productID == ProductId)
                {
                    cart.Remove(item);

                    break;
                }
            }

            Session["cart"] = cart;
            return Redirect("Index");
        }
        public ActionResult ViewOrderConfirm()
        {
            return View();
        }
        public ActionResult SubmitOrderDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitOrderDetails(Tbl_OrdererDetails order, Tbl_OrderProDetails pro)
        {


            if (Session["UserName"] != null)
            {
                
                int memberid = Convert.ToInt32(Session["memberID"]);
                order.memberID = memberid;
                order.date_time = DateTime.Now;

                _unitofWork.GetRepositoryInstance<Tbl_OrdererDetails>().Add(order);




                int finalTotal = 0;
                foreach (Item item in (List<Item>)Session["cart"])
                {
                    int Total = Convert.ToInt32(item.Quantity * item.Product.price);
                    finalTotal = Convert.ToInt32(finalTotal + Total);
                    pro.order_t_price = Total;
                    pro.order_product = item.Product.productName;
                    pro.memberID = memberid;
                    (pro.order_qty) = item.Quantity;

                    pro.order_price = item.Product.price;

                    pro.date_time = DateTime.Now;
                    pro.order_id = order.order_id;
                    _unitofWork.GetRepositoryInstance<Tbl_OrderProDetails>().Add(pro);
                }



                return RedirectToAction("SubmitOrderDetails");

            }
            else
            {
                return Redirect("Login");
            }

        }
      
        }
    }

