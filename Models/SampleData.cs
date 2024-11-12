using System.Linq;

namespace lab5.Models
{
    public static class SampleData
    {
        public static void Initialize(carRentContext context)
        {
            if (!context.Cars.Any())
            {
                context.Cars.AddRange(
                    new Car
                    {
                        brand = "Audi",
                        model = "A6",
                        carNumber = "BI4171CA",
                        pricePerDay = 100,
                        isRented = false
                    },
                    new Car
                    {
                        brand = "BMW",
                        model = "X5",
                        carNumber = "AX4123KA",
                        pricePerDay = 99,
                        isRented = false
                    },
                    new Car
                    {
                        brand = "BMW",
                        model = "X5",
                        carNumber = "KA1234KA",
                        pricePerDay = 99,
                        isRented = false
                    }
                );

                context.SaveChanges();
            }

            if (!context.Clients.Any())
            {
                context.Clients.AddRange(
                    new Client
                    {
                        name = "Kiril",
                        age = 19
                    },
                    new Client
                    {
                        name = "Ihor",
                        age = 20
                    }
                );

                context.SaveChanges();
            }

            // Добавление документов, если они еще не существуют
            if (!context.Documents.Any())
            {
                var clients = context.Clients.ToList();
                var cars = context.Cars.ToList();

                context.Documents.AddRange(
                    new Document
                    {
                        client = clients[0], // "Kiril"
                        car = cars[0], // "Audi A6"
                        startDate = DateTime.Now,
                        endDate = DateTime.Now.AddDays(3)
                    },
                    new Document
                    {
                        client = clients[1], // "Ihor"
                        car = cars[1], // "BMW X5"
                        startDate = DateTime.Now,
                        endDate = DateTime.Now.AddDays(5)
                    },
                    new Document
                    {
                        client = clients[0], // "Kiril"
                        car = cars[2], // "BMW X5" (второй экземпляр)
                        startDate = DateTime.Now,
                        endDate = DateTime.Now.AddDays(7)
                    }
                );

                // Обновление статуса аренды автомобилей
                cars[0].isRented = true;
                cars[1].isRented = true;
                cars[2].isRented = true;

                context.SaveChanges();
            }
        }
    }
}
