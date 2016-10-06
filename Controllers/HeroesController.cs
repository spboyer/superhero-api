using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenFu;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace superhero
{
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
        [HttpGet("{firstName}/{lastName}")]
        public Person Get(string firstName, string lastName)
        {
            var p = new Person(){FirstName = firstName, LastName = lastName};
            p.SetHeroName();

            return p;
        }

        [HttpGet()]
        public Person Get()
        {
            var p = A.New<Person>();
            p.SetHeroName();

            return p;
        }
    }
}
