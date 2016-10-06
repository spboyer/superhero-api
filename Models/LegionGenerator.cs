using System.Collections.Generic;
using GenFu;

namespace superhero
{
    public static class LegionGenerator
    {
        public static List<Person> GetLegion(int numberOfMembers)
        {
            var legion = A.ListOf<Person>(numberOfMembers);
            legion.ForEach(p => p.SetHeroName());
            return legion;
        }
    }
}
