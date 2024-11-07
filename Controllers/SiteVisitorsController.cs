using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using lab5.Models;

namespace webapiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteVisitorsController : ControllerBase
    {
        private static List<SiteVisitorStats> data = new List<SiteVisitorStats>();
        private static Random rng = new Random();

        public SiteVisitorsController()
        {
            // Генерируем начальные данные, если список пуст
            if (!data.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    data.Add(new SiteVisitorStats
                    {
                        Date = DateTime.Now.AddSeconds(-i),
                        VisitorCount = rng.Next(20, 100)
                    });
                }
            }
        }

        [HttpGet("visitor-stats")]
        public IEnumerable<SiteVisitorStats> Get()
        {
            // Создаем новые данные и добавляем их в список
            var newData = new SiteVisitorStats
            {
                Date = DateTime.Now,
                VisitorCount = rng.Next(20, 100)
            };

            // Добавляем новые данные в список и удаляем старые, если их больше 10
            data.Add(newData);
            if (data.Count > 10)
            {
                data.RemoveAt(0); // Удаляем первую точку
            }

            return data;
        }
    }
}
