using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Bee
{
    public class SpeciesCombiner : MonoBehaviour
    {
        void Start()
        {
            EnumBeeSpecies[] test = new EnumBeeSpecies[] { EnumBeeSpecies.MEADOWS, EnumBeeSpecies.MEADOWS };


            var values = CombinableDictionary.AccesDictionary(test);

            for (int i = 0; i < values.Length; i++)
            {
                print(values[i]);
            }
        }
    }
}