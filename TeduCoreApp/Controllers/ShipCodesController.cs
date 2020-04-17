using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ShipCodesController : Controller
    {
        private readonly AppDbContext _context;

        public ShipCodesController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public IActionResult GetAddressId()
        //{
        //    var id = _context.Provinces.Where(x => x.Name == "Thành phố Hồ Chí Minh").Select(x => x.Id).FirstOrDefault();
        //    return new OkObjectResult(id);
        //}

        // GET: api/ShipCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipCode>>> GetShipCodes()
        {
            return new OkObjectResult(await _context.ShipCodes.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<ShipCode>> GetShipCodeById(string IdShipCode)
        {
            return new OkObjectResult(await _context.ShipCodes.Where(x => x.Id.ToString() == IdShipCode).SingleOrDefaultAsync());
        }

        // GET: api/ShipCodes/5
        [HttpGet]
        public async Task<ActionResult<ShipCode>> GetShipCode(int idadress)
        {
            var shipCode = await _context.ShipCodes.Where(x => x.IdAddress == idadress).SingleOrDefaultAsync();

            if (shipCode == null)
            {
                return NotFound();
            }

            return new OkObjectResult(shipCode);
        }

        [HttpGet]
        public IActionResult GetShipCodeIdAdress(string NameProvince, string NameDistrict)
        {
            var IdProvince = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();
            var IdDistrict = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();

            int idAddress = _context.Address.Where(x => x.ProvinceId == IdProvince && x.DistrictId == IdDistrict).Select(x => x.Id).FirstOrDefault();

            return new OkObjectResult(_context.ShipCodes.Where(x => x.IdAddress == idAddress).FirstOrDefault());
        }

        [HttpGet]
        public IActionResult GetListShipCodeIdAdress(string NameProvince, string NameDistrict)
        {
            var IdProvince = _context.Provinces.Where(x => x.Name == NameProvince).Select(x => x.Id).FirstOrDefault();
            var IdDistrict = _context.Districts.Where(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();

            int idAddress = _context.Address.Where(x => x.ProvinceId == IdProvince && x.DistrictId == IdDistrict).Select(x => x.Id).FirstOrDefault();

            return new OkObjectResult(_context.ShipCodes.Where(x => x.IdAddress == idAddress).ToList());
        }

        private bool ShipCodeExists(long id)
        {
            return _context.ShipCodes.Any(e => e.Id == id);
        }
    }
}