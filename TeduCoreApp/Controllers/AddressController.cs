using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Controllers
{
    public class AddressController : Controller
    {
        //private readonly IAddressService _addressServices;
        private readonly AppDbContext _context;

        public AddressController(/*IAddressService addressServices,*/ AppDbContext context)
        {
            //_addressServices = addressServices;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX Request Province

        //[HttpPost]
        //public async Task<IActionResult> UpdateMutileWard()
        //{
        //    var tolistProvince = await _context.Provinces.ToListAsync();
        //    var tolistDistrict = await _context.Districts.ToListAsync();
        //    foreach (var item in tolistProvince)
        //    {
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult GetAllProvince()
        //{
        //    var list = _addressServices.GetAllProvince();
        //    return new OkObjectResult(list);
        //}

        //Provinces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces()
        {
            return new OkObjectResult(await _context.Provinces.ToListAsync());
        }

        public async Task<ActionResult<Province>> GetProvincesByKeyString(string KeyString)
        {
            var list = await _context.Provinces.Where(x => x.Name.Contains(KeyString)).ToListAsync();
            return new OkObjectResult(list);
        }

        [HttpGet]
        public async Task<ActionResult<Province>> GetProvinceByWard(string NameWard)
        {
            int IdProvince = _context.Wards.Where(x => x.Name == NameWard).Select(x => x.ProvinceId).FirstOrDefault();
            return new OkObjectResult(await _context.Provinces.Where(x => x.Id == IdProvince).FirstOrDefaultAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Province>> GetProvinceByDistrict(string NameDistrict)
        {
            int IdProvince = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.ProvinceId).FirstOrDefault();
            return new OkObjectResult(await _context.Provinces.Where(x => x.Id == IdProvince).FirstOrDefaultAsync());
        }

        #endregion AJAX Request Province

        #region AJAX Request District

        //Districts
        //Get List District
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistricts()
        {
            return new OkObjectResult(await _context.Districts.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictsByNameProvince(string NameProvince)
        {
            int IdProvince = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Districts.Where(x => x.ProvinceId == IdProvince).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictsByNameWard(string NameWard)
        {
            int IdDistrict = _context.Wards.Where(x => x.Name == NameWard).Select(x => x.DistrictId).FirstOrDefault();
            return new OkObjectResult(await _context.Districts.Where(x => x.Id == IdDistrict).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictsByKeyString(string KeyString)
        {
            return new OkObjectResult(await _context.Districts.Where(x => x.Name.Contains(KeyString)).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictsByKeyStringAndNameProvince(string KeyString, string NameProvince)
        {
            int IdProvince = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Districts.Where(x => x.Name.Contains(KeyString) && x.ProvinceId == IdProvince).ToListAsync());
        }

        //Get District
        //Get DistrictById
        [HttpGet]
        public async Task<ActionResult<District>> GetDistrictByWard(string NameWard)
        {
            int IdDistrict = _context.Wards.Where(x => x.Name == NameWard).Select(x => x.DistrictId).FirstOrDefault();
            return new OkObjectResult(await _context.Districts.Where(x => x.Id == IdDistrict).FirstOrDefaultAsync());
        }

        #endregion AJAX Request District

        #region AJAX Request Ward

        //Wards
        //Key Ward
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByKeyString(string KeyString)
        {
            return new OkObjectResult(await _context.Wards.Where(x => x.Name.Contains(KeyString)).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByKeyStringAndNameProvince(string KeyString, string NameProvince)
        {
            int IdProvince = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Wards.Where(x => x.Name.Contains(KeyString) && x.ProvinceId == IdProvince).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByKeyStringAndNameDistrict(string KeyString, string NameDistrict)
        {
            int IdDistrict = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Wards.Where(x => x.Name.Contains(KeyString) && x.ProvinceId == IdDistrict).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByKeyStringAndNameDistrictAndNameProvince(string KeyString, string NameDistrict, string NameProvince)
        {
            int IdDistrict = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();
            int IdProvince = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Wards.Where(x => x.Name.Contains(KeyString) && x.ProvinceId == IdDistrict && x.ProvinceId == IdProvince).ToListAsync());
        }

        //Click Load Data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByNameProvince(string NameProvince)
        {
            int IdDistrict = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Wards.Where(x => x.ProvinceId == IdDistrict).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByNameDistrict(string NameDistrict)
        {
            int IdDistrict = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Wards.Where(x => x.DistrictId == IdDistrict).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByNameDistrictAndNameProvince(string NameDistrict, string NameProvince)
        {
            int IdDistrict = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();
            int IdProvince = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();

            return new OkObjectResult(await _context.Wards.Where(x => x.DistrictId == IdDistrict && x.ProvinceId == IdProvince).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWards()
        {
            return new JsonResult(await _context.Wards.Select(x => x.Name).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Ward>> GetWardByNameStreet(string NameStreet)
        {
            int IdWard = _context.Streets.Where(x => x.Name == NameStreet).Select(x => x.WardId).FirstOrDefault();
            return new OkObjectResult(await _context.Wards.Where(x => x.Id == IdWard).FirstOrDefaultAsync());
        }

        #endregion AJAX Request Ward

        #region AJAX Request Street

        //Streets
        [HttpGet]
        public async Task<IActionResult> GetStreets()
        {
            return new OkObjectResult(await _context.Streets.Select(x => x.Name).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByNameWard(string NameWard)
        {
            int IdWard = _context.Wards.Where(x => x.Name == NameWard).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Streets.Where(x => x.WardId == IdWard).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByKeyString(string KeyString)
        {
            return new OkObjectResult(await _context.Streets.Where(x => x.Name.Contains(KeyString)).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByNameWardAndNameDistrict(string NameDistrict, string NameWard)
        {
            int IdDistrict = await _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).SingleOrDefaultAsync();
            int IdWard = await _context.Wards.Where(x => x.Name == NameWard).Select(x => x.Id).SingleOrDefaultAsync();

            return new OkObjectResult(await _context.Streets.Where(x => x.DistrictId == IdDistrict && x.WardId == IdWard).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByNameProvinceAndNameDistrict(string NameDistrict, string NameProvince)
        {
            int IdDistrict = await _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).SingleOrDefaultAsync();
            int IdProvince = await _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).SingleOrDefaultAsync();

            return new OkObjectResult(await _context.Streets.Where(x => x.DistrictId == IdDistrict && x.ProvinceId == IdProvince).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByKeyStringByDistrictWard(string KeyString, string NameDistrict, string NameWard)
        {
            int IdDistrict = await _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).SingleOrDefaultAsync();
            int IdWard = await _context.Wards.Where(x => x.Name == NameWard).Select(x => x.Id).SingleOrDefaultAsync();

            return new OkObjectResult(await _context.Streets.Where(x => x.Name.Contains(KeyString) && x.DistrictId == IdDistrict && x.WardId == IdWard).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByKeyStringByProvinceDistrict(string KeyString, string NameDistrict, string NameProvince)
        {
            int IdDistrict = await _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).SingleOrDefaultAsync();
            int IdProvince = await _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).SingleOrDefaultAsync();
            return new OkObjectResult(await _context.Streets.Where(x => x.Name.Contains(KeyString) && x.DistrictId == IdDistrict && x.ProvinceId == IdProvince).ToListAsync());
        }

        #endregion AJAX Request Street
    }
}