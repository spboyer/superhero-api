using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace superhero
{
    public class Legion
    {
        public string Guid{ get; set;}
        public DateTime Expires {get;set;}
        public string Issuer {get;set;}

        public List<Person> Team {get;set;}

        public Legion()
        {
        }
    }
}
