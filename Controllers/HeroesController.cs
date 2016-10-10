using Microsoft.AspNetCore.Mvc;
using GenFu;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace superhero
{
    /// <summary>
    /// API endpoint for generating random Superhero or getting a Superhero name for a Persons name
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")] 
    public class HeroesController : Controller
    {
        /// <summary>
        /// Returns a Person object with the Superhero name generated
        /// </summary>
        /// <param name="firstName">First Name of the Person object</param>
        /// <param name="lastName">Last Name of the Person object</param>
        /// <returns>New Person object</returns>
        /// <response code="200"></response>
        [ProducesResponseTypeAttribute(typeof(Person), 200)]
        [HttpGet("{firstName}/{lastName}")]
        public Person Get(string firstName, string lastName)
        {
            var p = new Person(){FirstName = firstName, LastName = lastName};
            p.SetHeroName();

            return p;
        }

        /// <summary>
        /// Returns a random Person object using GenFu
        /// </summary>
        /// <remarks>
        /// GenFu is an Open Source library for creating contextual based test data
        /// is available at https://github.com/MisterJames/GenFu
        /// </remarks>
        /// <returns>New Person object</returns>
        /// <response code="200"></response>
        [HttpGet()]
        public Person Get()
        {
            var p = A.New<Person>();
            p.SetHeroName();

            return p;
        }
    }
}
