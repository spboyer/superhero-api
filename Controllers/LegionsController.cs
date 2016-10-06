using System;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace superhero
{
    [Route("api/[controller]")]
    public class LegionsController : Controller
    {
        // GET api/values
        [HttpGet("{numberOfMembers}")]
        public Legion Get(int numberOfMembers)
        {
        
            var legion = new Legion()
            {
                Team = LegionGenerator.GetLegion(numberOfMembers),
                Guid = Guid.NewGuid().ToString(),
                Issuer = Environment.MachineName,
                Expires = DateTime.UtcNow.AddHours(1)
            };
                    

            return legion;
        }

    }
}
