using System.Collections.Generic;
using Bees.Genetics.Enums;

namespace Bees.Genetics
{
    public class EqualityComparer : IEqualityComparer<BeeSpecies[]>
    {
        BeeSpecies[] keyToFind;

        public bool Equals(BeeSpecies[] x, BeeSpecies[] y)
        {
            if(x.Length != y.Length)
            {
                return false;
            }

            for(int i = 0; i < x.Length; i++)
            {
                if(x[i] != y[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(BeeSpecies[] obj)
        {
            int result = 17;

            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + i;
                }
            }
            return result;
        }
    }
}
