using System;
using Microsoft.AspNetCore.Mvc;

namespace superhero
{
    /// <summary>
    /// API endpoint for generating random Legion of Superheroes
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LegionsController : Controller
    {
        
        /// <summary>
        /// Creates a Legion object with a Team equal to the amount of members requested
        /// </summary>
        /// <param name="numberOfMembers">Number of Person objects to include in Team</param>
        [ProducesResponseTypeAttribute(typeof(Legion), 200)]
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
