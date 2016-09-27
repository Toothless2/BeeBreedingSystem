using System.Collections.Generic;

namespace Bee
{
    public class EqualityComparer : IEqualityComparer<EnumBeeSpecies[]>
    {
        EnumBeeSpecies[] keyToFind;

        public bool Equals(EnumBeeSpecies[] x, EnumBeeSpecies[] y)
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

        public int GetHashCode(EnumBeeSpecies[] obj)
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
