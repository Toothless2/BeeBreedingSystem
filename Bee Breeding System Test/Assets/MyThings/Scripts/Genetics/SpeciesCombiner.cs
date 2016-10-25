using UnityEngine;
using System.Collections.Generic;
using Bees.Genetics.Enums;
using Bees.Entity;

namespace Bees.Genetics
{
    public static class SpeciesCombiner
    {
        public static BeeSpecies[] CombineSpecies(BeeSpecies bee1, BeeSpecies bee2)
        {
            BeeSpecies[] inputbees = new BeeSpecies[2] { bee1, bee2 };

            return CombinableDictionary.AccesDictionary(inputbees);
        }
    }
}