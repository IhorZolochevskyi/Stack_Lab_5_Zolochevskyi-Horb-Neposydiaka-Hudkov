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
        public IActionResult GetParkById(int id)
        {
            var park = _db.Parks.FirstOrDefault(p => p.Id == id);
            if (park == null)
            {
                return NotFound();
            }
            return Json(park);
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
        public IActionResult GetPlantingById(int id)
        {
            var planting = _db.Plantings.FirstOrDefault(p => p.Id == id);
            if (planting == null)
            {
                return NotFound();
            }
            return Json(planting);
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
        public IActionResult GetFountainById(int id)
        {
            var fountain = _db.Fountains.FirstOrDefault(p => p.Id == id);
            if (fountain == null)
            {
                return NotFound();
            }
            return Json(fountain);
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

        [HttpGet]
        public IActionResult GetPavilionById(int id)
        {
            var pavilion = _db.Pavilions.FirstOrDefault(p => p.Id == id);
            if (pavilion == null)
            {
                return NotFound();
            }
            return Json(pavilion);
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
        public IActionResult ChangePark(int parkId, string name, decimal square, string location)
        {
            var park = _db.Parks.FirstOrDefault(p => p.Id == parkId);
            if (park == null)
            {
                return NotFound();
            }
            park.Name = name;
            park.Square = square;
            park.Location = location;
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult DeletePark(int id)
        {
            var park = _db.Parks.Find(id);
            if (park == null)
            {
                return NotFound();
            }

            _db.Parks.Remove(park);
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
        public IActionResult ChangePlanting(int plantingId, int parkId, string cultureType, string name, int averageLifetime, int quantity)
        {
            var planting = _db.Plantings.FirstOrDefault(p => p.Id == plantingId);
            if (planting == null)
            {
                return NotFound();
            }
            planting.ParkId = parkId;
            planting.CultureType = cultureType;
            planting.Name = name;
            planting.AverageLifetime = averageLifetime;
            planting.Quantity = quantity;
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult DeletePlanting(int id)
        {
            var planting = _db.Plantings.Find(id);
            if (planting == null)
            {
                return NotFound();
            }

            _db.Plantings.Remove(planting);
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
        public IActionResult ChangeFountain(int fountainId, int parkId, int code, DateTime buildDate, decimal maxWaterConsumption, decimal normalWaterConsumption, decimal square)
        {
            var fountain = _db.Fountains.FirstOrDefault(p => p.Id == fountainId);
            if (fountain == null)
            {
                return NotFound();
            }
            fountain.ParkId = parkId;
            fountain.Code = code;
            fountain.BuildDate = buildDate;
            fountain.MaxWaterConsumption = maxWaterConsumption;
            fountain.NormalWaterConsumption = normalWaterConsumption;
            fountain.Square = square;
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteFountain(int id)
        {
            var fountain = _db.Fountains.Find(id);
            if (fountain == null)
            {
                return NotFound();
            }

            _db.Fountains.Remove(fountain);
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

        [HttpPost]
        public IActionResult ChangePavilion(int pavilionId, int parkId, string name, string type, decimal square)
        {
            var pavilion = _db.Pavilions.FirstOrDefault(p => p.Id == pavilionId);
            if (pavilion == null)
            {
                return NotFound();
            }
            pavilion.ParkId = parkId;
            pavilion.Name = name;
            pavilion.Type = type;
            pavilion.Square = square;
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult DeletePavilion(int id)
        {
            var pavilion = _db.Pavilions.Find(id);
            if (pavilion == null)
            {
                return NotFound();
            }

            _db.Pavilions.Remove(pavilion);
            _db.SaveChanges();

            return Ok();
        }
    }
}
