using System;
using System.Collections.Generic;

namespace superhero
{
    /// <summary>
    /// Legion object to represent random data from random machine
    /// </summary>    
    public class Legion
    {
        /// <summary>
        /// New Guid
        /// </summary>        
        public string Guid{ get; set;}
        
        /// <summary>
        /// Set to DateTime.Now + 1 hour
        /// </summary>        
        public DateTime Expires {get;set;}
       
        /// <summary>
        /// Environment.Machinename
        /// </summary>
        public string Issuer {get;set;}

        /// <summary>
        /// List of Person objects
        /// </summary>
        public List<Person> Team {get;set;}

    }
}
