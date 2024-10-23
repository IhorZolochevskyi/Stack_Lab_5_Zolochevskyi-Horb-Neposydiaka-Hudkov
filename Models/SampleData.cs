using System.Linq;
using System.Data;

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
                        pricePerDay = 100
                    },
                    new Car
                    {
                        brand = "BMW",
                        model = "X5",
                        carNumber = "AX4123KA",
                        pricePerDay = 99
                    },
                    new Car
                    {
                        brand = "BMW",
                        model = "X5",
                        carNumber = "KA1234KA",
                        pricePerDay = 99
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
        }
    }
}
