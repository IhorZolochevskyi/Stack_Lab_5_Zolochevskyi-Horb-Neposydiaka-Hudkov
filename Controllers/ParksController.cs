using Microsoft.AspNetCore.Mvc;
using lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace lab5.Controllers
{
    public class ParksController : Controller
    {
        private readonly ParksContext _db;

        public ParksController(ParksContext ctx)
        {
            _db = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllParks()
        {
            var parks = _db.Parks.ToList();
            return Json(parks);
        }

        [HttpGet]
        public JsonResult GetAllPlantings()
        {
            var plantings = _db.Plantings.Include(p => p.Park).ToList();
            return Json(plantings.Select(p => new
            {
                p.Id,
                p.CultureType,
                p.Name,
                p.AverageLifetime,
                p.Quantity,
                ParkName = p.Park != null ? p.Park.Name : "Unknown"
            }));
        }

        [HttpGet]
        public JsonResult GetAllFountains()
        {
            var fountains = _db.Fountains.Include(f => f.Park).ToList();
            return Json(fountains.Select(f => new
            {
                f.Id,
                f.Code,
                f.BuildDate,
                f.MaxWaterConsumption,
                f.NormalWaterConsumption,
                f.Square,
                ParkName = f.Park != null ? f.Park.Name : "Unknown"
            }));
        }

        [HttpGet]
        public JsonResult GetAllPavilions()
        {
            var pavilions = _db.Pavilions.Include(p => p.Park).ToList();
            return Json(pavilions.Select(p => new
            {
                p.Id,
                p.Name,
                p.Type,
                p.Square,
                ParkName = p.Park != null ? p.Park.Name : "Unknown"
            }));
        }

        [HttpPost]
        public IActionResult AddPark(string name, decimal square, string location)
        {
            if (string.IsNullOrEmpty(name) || square <= 0 || string.IsNullOrEmpty(location))
            {
                return BadRequest("Invalid park data.");
            }

            Park park = new Park
            {
                Name = name,
                Square = square,
                Location = location
            };

            _db.Parks.Add(park);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult AddPlanting(int parkId, string cultureType, string name, int averageLifetime, int quantity)
        {
            if (parkId <= 0 || string.IsNullOrEmpty(cultureType) || string.IsNullOrEmpty(name) || averageLifetime <= 0 || quantity <= 0)
            {
                return BadRequest("Invalid planting data.");
            }

            Planting planting = new Planting
            {
                ParkId = parkId,
                CultureType = cultureType,
                Name = name,
                AverageLifetime = averageLifetime,
                Quantity = quantity
            };

            _db.Plantings.Add(planting);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult AddFountain(int parkId, int code, DateTime buildDate, decimal maxWaterConsumption, decimal normalWaterConsumption, decimal square)
        {
            if (parkId <= 0 || code <= 0 || buildDate == default || maxWaterConsumption <= 0 || normalWaterConsumption <= 0 || square <= 0)
            {
                return BadRequest("Invalid fountain data.");
            }

            Fountain fountain = new Fountain
            {
                ParkId = parkId,
                Code = code,
                BuildDate = buildDate,
                MaxWaterConsumption = maxWaterConsumption,
                NormalWaterConsumption = normalWaterConsumption,
                Square = square
            };

            _db.Fountains.Add(fountain);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult AddPavilion(int parkId, string name, string type, decimal square)
        {
            if (parkId <= 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type) || square <= 0)
            {
                return BadRequest("Invalid pavilion data.");
            }

            Pavilion pavilion = new Pavilion
            {
                ParkId = parkId,
                Name = name,
                Type = type,
                Square = square
            };

            _db.Pavilions.Add(pavilion);
            _db.SaveChanges();

            return Ok();
        }
    }
}
