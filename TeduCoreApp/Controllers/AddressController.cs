using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressServices;
        private readonly AppDbContext _context;

        public AddressController(IAddressService addressServices, AppDbContext context)
        {
            _addressServices = addressServices;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX Request Province

        [HttpGet]
        public ActionResult GetProvinces()
        {
            return new OkObjectResult(_addressServices.GetProvinces());
        }

        public IActionResult GetProvincesByKeyString(string KeyString)
        {
            return new OkObjectResult(_addressServices.GetProvincesByKeyString(KeyString));
        }

        [HttpGet]
        public IActionResult GetProvinceByNameDistrict(string NameDistrict)
        {
            return new OkObjectResult(_addressServices.GetProvinceByNameDistrict(NameDistrict));
        }

        [HttpGet]
        public IActionResult GetProvinceByNameWard(string NameWard)
        {
            return new OkObjectResult(_addressServices.GetProvinceByNameWard(NameWard));
        }

        #endregion AJAX Request Province

        #region AJAX Request District

        [HttpGet]
        public ActionResult GetDistricts()
        {
            return new OkObjectResult(_addressServices.GetDistricts());
        }

        [HttpGet]
        public IActionResult GetDistrictsByNameProvince(string NameProvince)
        {
            return new OkObjectResult(_addressServices.GetDistrictsByNameProvince(NameProvince));
        }

        [HttpGet]
        public IActionResult GetDistrictsByNameWard(string NameWard)
        {
            return new OkObjectResult(_addressServices.GetDistrictsByNameWard(NameWard));
        }

        [HttpGet]
        public IActionResult GetDistrictsByKeyString(string KeyString)
        {
            return new OkObjectResult(_addressServices.GetDistrictsByKeyString(KeyString));
        }

        [HttpGet]
        public IActionResult GetDistrictsByKeyStringAndNameProvince(string KeyString, string NameProvince)
        {
            return new OkObjectResult(_addressServices.GetDistrictsByKeyStringAndNameProvince(KeyString, NameProvince));
        }

        #endregion AJAX Request District

        #region AJAX Request Ward

        [HttpGet]
        public IActionResult GetWards()
        {
            return new JsonResult(_addressServices.GetWards());
        }

        [HttpGet]
        public IActionResult GetWardsByKeyString(string KeyString)
        {
            return new OkObjectResult(_addressServices.GetWardsByKeyString(KeyString));
        }

        [HttpGet]
        public IActionResult GetWardsByKeyStringAndNameProvince(string KeyString, string NameProvince)
        {
            return new OkObjectResult(_addressServices.GetDistrictsByKeyStringAndNameProvince(KeyString, NameProvince));
        }

        [HttpGet]
        public IActionResult GetWardsByKeyStringAndNameDistrict(string KeyString, string NameDistrict)
        {
            return new OkObjectResult(_addressServices.GetWardsByKeyStringAndNameDistrict(KeyString, NameDistrict));
        }

        //Click Load Data
        [HttpGet]
        public IActionResult GetWardsByNameProvince(string NameProvince)
        {
            return new OkObjectResult(_addressServices.GetWardsByNameProvince(NameProvince));
        }

        [HttpGet]
        public IActionResult GetWardsByNameDistrict(string NameDistrict)
        {
            return new OkObjectResult(_addressServices.GetWardsByNameDistrict(NameDistrict));
        }

        [HttpGet]
        public IActionResult GetWardByNameStreet(string NameStreet)
        {
            return new OkObjectResult(_addressServices.GetWardByNameStreet(NameStreet));
        }

        #endregion AJAX Request Ward

        #region AJAX Request Street

        //Streets
        [HttpGet]
        public IActionResult GetStreets()
        {
            return new OkObjectResult(_addressServices.GetStreets());
        }

        [HttpGet]
        public IActionResult GetStreetsByKeyString(string KeyString)
        {
            return new OkObjectResult(_addressServices.GetStreetsByKeyString(KeyString));
        }

        [HttpGet]
        public IActionResult GetStreetsByNameWard(string NameWard)
        {
            return new OkObjectResult(_addressServices.GetStreetsByNameWard(NameWard));
        }

        [HttpGet]
        public IActionResult GetStreetsByNameDistrict(string NameDistrict)
        {
            return new OkObjectResult(_addressServices.GetStreetsByNameDistrict(NameDistrict));
        }

        [HttpGet]
        public IActionResult GetStreetsByKeyStringAndByNameWard(string KeyString, string NameWard)
        {
            return new OkObjectResult(_addressServices.GetStreetsByKeyStringAndByNameWard(KeyString, NameWard));
        }

        [HttpGet]
        public IActionResult GetStreetsByKeyStringAndByNameDistrict(string KeyString, string NameDistrict)
        {
            return new OkObjectResult(_addressServices.GetStreetsByKeyStringAndByNameDistrict(KeyString, NameDistrict));
        }

        #endregion AJAX Request Street
    }
}