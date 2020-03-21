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

        //Districts
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
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictsByKeyString(string KeyString)
        {
            return new OkObjectResult(await _context.Districts.Where(x => x.Name.Contains(KeyString)).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictsByNameWard(string NameWard)
        {
            int IdDistrict = _context.Wards.Where(x => x.Name == NameWard).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Districts.Where(x => x.Id == IdDistrict).ToListAsync());
        }

        //Wards

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByKeyString(string KeyString)
        {
            return new OkObjectResult(await _context.Wards.Where(x => x.Name.Contains(KeyString)).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsByNameDistrict(string NameDistrict)
        {
            int IdDistrict = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();
            return new OkObjectResult(await _context.Wards.Where(x => x.DistrictId == IdDistrict).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWards()
        {
            return new JsonResult(await _context.Wards.Select(x => x.Name).ToListAsync());
        }

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
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByIdDistrict(/*int IdDistrict*/)
        {
            int IdDistrict = 1;
            return new OkObjectResult(await _context.Streets.Where(x => x.DistrictId == IdDistrict).Select(x => x.Name).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreetsByIdProvince(/*int IdProvince*/)
        {
            int IdProvince = 1;
            return new OkObjectResult(await _context.Streets.Where(x => x.ProvinceId == IdProvince).Select(x => x.Name).ToListAsync());
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
    }
}