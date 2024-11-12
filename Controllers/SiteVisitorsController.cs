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
            var newData = new SiteVisitorStats
            {
                Date = DateTime.Now,
                VisitorCount = rng.Next(20, 100)
            };

            data.Add(newData);
            if (data.Count > 10)
            {
                data.RemoveAt(0);
            }

            return data;
        }
    }
}