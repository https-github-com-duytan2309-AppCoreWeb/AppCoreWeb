using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Models;
using TeduCoreApp.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using TeduCoreApp.Extensions;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Enums;
using System.Security.Claims;
using TeduCoreApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using TeduCoreApp.Data.Entities;
using System.Net.Mail;
using System.Net.Mime;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.EF;

namespace TeduCoreApp.Controllers
{
    public class CartController : Controller
    {
        private IProductService _productService;
        private IBillService _billService;
        private IViewRenderService _viewRenderService;
        private IConfiguration _configuration;
        private IEmailSender _emailSender;

        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _disctrictRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IStreetRepository _streetRepository;
        private readonly IAddressService _addressServices;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartController(IProductService productService,
            IViewRenderService viewRenderService, IEmailSender emailSender,
            IConfiguration configuration, IBillService billService,
            IAddressService addressServices,
            IProvinceRepository provinceRepository,
            IDistrictRepository disctrictRepository,
            IWardRepository wardRepository,
            IStreetRepository streetRepository,
             AppDbContext context,
            UserManager<AppUser> userManager
            )
        {
            _productService = productService;
            _billService = billService;
            _viewRenderService = viewRenderService;
            _configuration = configuration;
            _emailSender = emailSender;

            _provinceRepository = provinceRepository;
            _disctrictRepository = disctrictRepository;
            _wardRepository = wardRepository;
            _streetRepository = streetRepository;
            _addressServices = addressServices;
            _context = context;
            _userManager = userManager;
        }

        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("checkout.html", Name = "Checkout")]
        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new CheckoutViewModel();

            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session.Any(x => x.Color == null || x.Size == null))
            {
                return Redirect("/cart.html");
            }
            model.Carts = session;
            return View(model);
        }

        [Route("checkout.html", Name = "Checkout")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            var random = new System.Random();
            if (ModelState.IsValid)
            {
                //if (model.ShipCodeId == 0)
                //{
                //    model.ShipCodeId = AddStreetAndShipCodeInCheckout(model.Province, model.District, model.Ward, model.Street);
                //}

                if (session != null)
                {
                    var details = new List<BillDetailViewModel>();
                    foreach (var item in session)
                    {
                        details.Add(new BillDetailViewModel()
                        {
                            Product = item.Product,
                            Price = item.Price,
                            ColorId = item.Color.Id,
                            SizeId = item.Size.Id,
                            Quantity = item.Quantity,
                            ProductId = item.Product.Id,
                        });
                    }
                    var billViewModel = new BillViewModel()
                    {
                        CustomerMobile = model.CustomerMobile,
                        BillStatus = BillStatus.New,
                        CustomerAddress = model.Street + ", " + model.Ward + ", " + model.District + ", " + model.Province,
                        CustomerName = model.CustomerName,
                        CustomerMessage = model.CustomerMessage,
                        BillDetails = details,
                        DateCreated = DateTime.Now,
                        Code = random.Next().ToString(),
                        ShipCodeId = model.ShipCodeId,
                        PaymentMethod = model.PaymentMethod
                    };

                    if (User.Identity.IsAuthenticated == true)
                    {
                        billViewModel.CustomerId = Guid.Parse(User.GetSpecificClaim("UserId"));
                    }
                    _billService.Create(billViewModel);
                    try
                    {
                        _billService.Save();
                        var content = await _viewRenderService.RenderToStringAsync("Cart/_BillMail", billViewModel);
                        var contentAdmin = await _viewRenderService.RenderToStringAsync("Cart/_BillMailForAdmin", billViewModel);
                        List<string> images = new List<string>();
                        foreach (var item in details)
                        {
                            images.Add(@item.Product.Image.Replace('/', '\\'));
                        }

                        var addmessage = "<h4>Tài Khoản " + User.FindFirst("Email").Value.ToString() + " Đã mua hàng của bạn!!! Click + <a href='/admin/bill/index'>Vào Đây</a> + để kiểm tra thông tin!</h4>";
                        await _emailSender.SendEmailBillMailAsync(User.FindFirst("Email").Value.ToString(), "Đơn hàng mới từ shop Thành Vượng", content, images);
                        await _emailSender.SendEmailBillMailAsync("binhit201195@gmail.com", "Đơn hàng mới", addmessage.ToString() + contentAdmin, images);
                        ViewData["Success"] = true;

                        //Clear All product In HeaderCart
                        HttpContext.Session.Remove(CommonConstants.CartSession);
                    }
                    catch (Exception ex)
                    {
                        ViewData["Success"] = false;
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }

            model.Carts = session;

            return View(model);
        }

        //[HttpPost]
        public long AddStreetAndShipCodeInCheckout(string Province, string NameDistrict, string NameWard, string Street)
        {
            var idProvince = _provinceRepository.FindSingle(x => x.Name == Province).Id;
            var idDistrict = _provinceRepository.FindSingle(x => x.Name == NameDistrict).Id;
            var idWard = _wardRepository.FindSingle(x => x.Name == NameWard).Id;

            StreetViewModel streetVm = new StreetViewModel();
            streetVm.Name = Street;
            streetVm.DistrictId = idDistrict;
            streetVm.WardId = idWard;
            streetVm.Status = false;
            _addressServices.CreateStreet(streetVm);
            _addressServices.Save();

            Address address = new Address();
            address.ProvinceId = idProvince;
            address.DistrictId = idDistrict;
            address.WardId = idWard;
            _context.Add(address);
            _context.SaveChanges();

            var idAddress = _context.Address.Where(x => x.WardId == idWard && x.DistrictId == idDistrict).FirstOrDefault();
            ShipCode ship = new ShipCode();
            ship.IdAddress = idAddress.Id;
            _context.Add(ship);
            _context.SaveChanges();

            var result = _context.ShipCodes.Where(x => x.IdAddress == ship.IdAddress).FirstOrDefault().Id;
            return result;
        }

        #region AJAX Request

        /// <summary>
        /// Get list item
        /// </summary>
        /// <returns>
        /// </returns>
        public IActionResult GetCart()
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session == null)
                session = new List<ShoppingCartViewModel>();
            return new OkObjectResult(session);
        }

        /// <summary>
        /// Remove all products in cart
        /// </summary>
        /// <returns>
        /// </returns>
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CommonConstants.CartSession);
            return new OkObjectResult("OK");
        }

        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="productId">
        /// </param>
        /// <param name="quantity">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity, int color, int size)
        {
            //Get product detail
            var product = _productService.GetById(productId);

            //Get session with item list from cart

            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                //Convert string to list object
                bool hasChanged = false;

                //Check exist with item product id
                if (session.Any(x => x.Product.Id == productId))
                {
                    foreach (var item in session)
                    {
                        //Update quantity for product if match product id
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                            item.Price = product.PromotionPrice ?? product.Price;
                            hasChanged = true;
                        }
                    }
                }
                else
                {
                    session.Add(new ShoppingCartViewModel()
                    {
                        Product = product,
                        Quantity = quantity,
                        Color = _billService.GetColor(color),
                        Size = _billService.GetSize(size),
                        Price = product.PromotionPrice ?? product.Price,
                        DateCreated = DateTime.Now
                    });
                    hasChanged = true;
                }

                //Update back to cart
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
            }
            else
            {
                //Add new cart
                var cart = new List<ShoppingCartViewModel>();

                cart.Add(new ShoppingCartViewModel()
                {
                    Product = product,
                    Quantity = quantity,
                    Color = _billService.GetColor(color),
                    Size = _billService.GetSize(size),
                    Price = product.PromotionPrice ?? product.Price,
                    DateCreated = DateTime.Now
                });
                HttpContext.Session.Set(CommonConstants.CartSession, cart);
            }
            return new OkObjectResult(productId);
        }

        /// <summary>
        /// Remove a product
        /// </summary>
        /// <param name="productId">
        /// </param>
        /// <returns>
        /// </returns>
        public IActionResult RemoveFromCart(int productId)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                bool hasChanged = false;
                foreach (var item in session)
                {
                    if (item.Product.Id == productId)
                    {
                        session.Remove(item);
                        hasChanged = true;
                        break;
                    }
                }
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
                return new OkObjectResult(productId);
            }
            return new EmptyResult();
        }

        /// <summary>
        /// Update product quantity
        /// </summary>
        /// <param name="productId">
        /// </param>
        /// <param name="quantity">
        /// </param>
        /// <returns>
        /// </returns>
        public IActionResult UpdateCart(int productId, int quantity, int color, int size)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                bool hasChanged = false;
                foreach (var item in session)
                {
                    if (item.Product.Id == productId)
                    {
                        var product = _productService.GetById(productId);
                        item.Product = product;
                        item.Size = _billService.GetSize(size);
                        item.Color = _billService.GetColor(color);
                        item.Quantity = quantity;
                        item.Price = product.PromotionPrice ?? product.Price;
                        item.DateModified = DateTime.Now;
                        hasChanged = true;
                    }
                }
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
                return new OkObjectResult(productId);
            }
            return new EmptyResult();
        }

        //ClearSessionCart
        //public IActionResult ClearSessionCart()
        //{
        //    var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);

        //    session.Clear();
        //    return new EmptyResult();
        //}

        [HttpGet]
        public IActionResult GetColors()
        {
            var colors = _billService.GetColors();
            return new OkObjectResult(colors);
        }

        [HttpGet]
        public IActionResult GetSizes()
        {
            var sizes = _billService.GetSizes();
            return new OkObjectResult(sizes);
        }

        #endregion AJAX Request
    }
}