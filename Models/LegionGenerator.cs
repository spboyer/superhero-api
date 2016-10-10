using System.Collections.Generic;
using GenFu;

namespace superhero
{
    /// <summary>
    /// Static class used to create a Legion object
    /// </summary>
    public static class LegionGenerator
    {
        /// <summary>
        /// Creates a List of Person ojects base on the number of members requested
        /// </summary>
        /// <param name="numberOfMembers">Number of Person objects to create using GenFu</param>
        public static List<Person> GetLegion(int numberOfMembers)
        {
            var legion = A.ListOf<Person>(numberOfMembers);
            legion.ForEach(p => p.SetHeroName());
            return legion;
        }
    }
}
