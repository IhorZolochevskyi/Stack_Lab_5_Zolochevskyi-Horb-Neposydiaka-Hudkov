using System.Linq;

namespace lab5.Models
{
    public static class SampleData
    {
        public static void Initialize(ParksContext context)
        {
            if (!context.Parks.Any())
            {
                context.Parks.AddRange(
                    new Park
                    {
                        Id = 1,
                        Name = "Central Park",
                        Square = 1000,
                        Location = "New York"
                    },
                    new Park
                    {
                        Id = 2,
                        Name = "Hyde Park",
                        Square = 2000,
                        Location = "London"
                    },
                    new Park
                    {
                        Id = 3,
                        Name = "Tuileries Garden",
                        Square = 3000,
                        Location = "Paris"
                    }
                );

                context.SaveChanges();
            }

            if (!context.Plantings.Any())
            {
                context.Plantings.AddRange(
                    new Planting
                    {
                        CultureType = "Tree",
                        Name = "Oak",
                        AverageLifetime = 100,
                        Quantity = 100,
                        ParkId = 1
                    },
                    new Planting
                    {
                        CultureType = "Flower",
                        Name = "Rose",
                        AverageLifetime = 5,
                        Quantity = 1000,
                        ParkId = 2
                    },
                    new Planting
                    {
                        CultureType = "Bush",
                        Name = "Juniper",
                        AverageLifetime = 10,
                        Quantity = 500,
                        ParkId = 3
                    }
                );

                context.SaveChanges();
            }

            if (!context.Fountains.Any())
            {
                context.Fountains.AddRange(
                    new Fountain
                    {
                        Code = 1,
                        BuildDate = new DateTime(2000, 1, 1),
                        MaxWaterConsumption = 100,
                        NormalWaterConsumption = 50,
                        Square = 10,
                        ParkId = 1
                    },
                    new Fountain
                    {
                        Code = 2,
                        BuildDate = new DateTime(2005, 1, 1),
                        MaxWaterConsumption = 200,
                        NormalWaterConsumption = 100,
                        Square = 20,
                        ParkId = 2
                    },
                    new Fountain
                    {
                        Code = 3,
                        BuildDate = new DateTime(2010, 1, 1),
                        MaxWaterConsumption = 300,
                        NormalWaterConsumption = 150,
                        Square = 30,
                        ParkId = 3
                    }
                );

                context.SaveChanges();
            }

            if (!context.Pavilions.Any())
            {
                context.Pavilions.AddRange(
                    new Pavilion
                    {
                        Name = "Grusha",
                        Type = "Restaurant",
                        Square = 100,
                        ParkId = 1
                    },
                    new Pavilion
                    {
                        Name = "777",
                        Type = "Product Shop",
                        Square = 50,
                        ParkId = 2
                    },
                    new Pavilion
                    {
                        Name = "MalyataHata",
                        Type = "Entertainment Center",
                        Square = 30,
                        ParkId = 3
                    },
                    new Pavilion
                    {
                        Name = "Stone Island",
                        Type = "Boutique",
                        Square = 20,
                        ParkId = 1
                    }
                );

                context.SaveChanges();
            }
        }
    }
}