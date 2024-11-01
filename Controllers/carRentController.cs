using Microsoft.AspNetCore.Mvc;
using lab5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace lab5.Controllers
{
    public class carRentController : Controller
    {
        carRentContext _db;
        public carRentController(carRentContext ctx)
        {
            _db = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllClients()
        {
            var clients = _db.Clients.ToList();
            return Json(clients);
        }

        [HttpGet]
        public JsonResult GetAllCars()
        {
            var cars = _db.Cars.ToList();

            return Json(cars);
        }

        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var documents = _db.Documents
                .Include(d => d.client)
                .Include(d => d.car)
                .Select(d => new
                {
                    id = d.Id,
                    client = d.client,
                    car = d.car,
                    startDate = d.startDate,
                    endDate = d.endDate
                })
                .ToList();

            return Json(documents);
        }
        [HttpGet]
        public JsonResult GetAvailableCarModels()
        {
            var availableCarModels = _db.Cars
                .Where(car => !car.isRented)
                .GroupBy(car => new { car.brand, car.model, car.pricePerDay })
                .Select(group => new
                {
                    brand = group.Key.brand,
                    model = group.Key.model,
                    pricePerDay = group.Key.pricePerDay
                })
                .ToList();

            return Json(availableCarModels);
        }
        [HttpPost]
        public IActionResult AddClient(string Name, int age)
        {
            if (string.IsNullOrEmpty(Name) || age <= 0)
            {
                return BadRequest("Невірні дані клієнта");
            }

            Client client = new Client
            {
                name = Name,
                age = age
            };

            _db.Clients.Add(client);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult AddCar(string brand, string model, string carNumber, decimal pricePerDay)
        {
            if (string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(carNumber) || pricePerDay <= 0)
            {
                return BadRequest("Невірні дані машини");
            }

            Car car = new Car
            {
                brand = brand,
                model = model,
                carNumber = carNumber,
                pricePerDay = pricePerDay
            };

            _db.Cars.Add(car);
            _db.SaveChanges();

            return Ok();
        }
        [HttpPost]
        public IActionResult AddDocument(string clientName, string carNumber, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(carNumber) || startDate == default || endDate == default || startDate >= endDate)
            {
                return BadRequest("Неправильні дані документа");
            }

            var client = _db.Clients.FirstOrDefault(c => c.name == clientName);
            if (client == null)
            {
                return NotFound("Клієнт не знайдений");
            }

            var car = _db.Cars.FirstOrDefault(c => c.carNumber == carNumber);
            if (car == null)
            {
                return NotFound("Машина не знайдена");
            }

            if (car.isRented)
            {
                return BadRequest("Машину вже орендовано");
            }

            Document document = new Document
            {
                client = client,
                car = car,
                startDate = startDate,
                endDate = endDate
            };

            car.isRented = true;

            _db.Documents.Add(document);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteClient(int id)
        {
            var client = _db.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound("Клієнт не знайдений");
            }

            var documents = _db.Documents.Where(d => d.client.Id == id).ToList();
            foreach (var document in documents)
            {
                var car = document.car;
                if (car != null)
                {
                    car.isRented = false;
                }
                _db.Documents.Remove(document);
            }

            _db.Clients.Remove(client);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteCar(int id)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var car = _db.Cars.FirstOrDefault(c => c.Id == id);
                    if (car == null)
                    {
                        return NotFound($"Машину з id {id} не знайдено.");
                    }

                    // Удаление всех связанных документов
                    var documents = _db.Documents.Where(d => d.car.Id == id).ToList();
                    foreach (var document in documents)
                    {
                        _db.Documents.Remove(document);
                    }

                    _db.Cars.Remove(car);
                    _db.SaveChanges();

                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Сталася помилка під час видалення машини: {ex.Message}");
                }
            }
        }

        [HttpPost]
        public IActionResult DeleteDocument(int id)
        {
            var document = _db.Documents.Include(d => d.car).FirstOrDefault(d => d.Id == id);
            if (document == null)
            {
                return NotFound("Документ не знайдено");
            }

            var car = document.car;
            if (car != null)
            {
                car.isRented = false;
            }

            _db.Documents.Remove(document);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult EditCar(int Id, string brand, string model, string carNumber, decimal pricePerDay)
        {
            if (Id <= 0 || string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(carNumber) || pricePerDay <= 0)
            {
                return BadRequest("Невірні дані машини.");
            }

            var car = _db.Cars.FirstOrDefault(c => c.Id == Id);
            if (car == null)
            {
                return NotFound($"Машину з id {Id} не знайдено.");
            }

            car.brand = brand;
            car.model = model;
            car.carNumber = carNumber;
            car.pricePerDay = pricePerDay;

            _db.SaveChanges();

            return Ok();
        }

    }
}
