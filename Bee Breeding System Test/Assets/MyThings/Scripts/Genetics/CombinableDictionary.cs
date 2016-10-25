using System;
using System.Collections.Generic;
using Bees.Genetics.Enums;

namespace Bees.Genetics
{
    public class CombinableDictionary
    {
        private static Dictionary<BeeSpecies[], BeeSpecies[]> Possiblechildren = new Dictionary<BeeSpecies[], BeeSpecies[]>(new EqualityComparer())
        {
            {SortArray(new BeeSpecies[] { BeeSpecies.FOREST, BeeSpecies.FOREST}), new BeeSpecies[] { BeeSpecies.FOREST} },
            {SortArray(new BeeSpecies[] { BeeSpecies.FOREST, BeeSpecies.MEADOWS}), new BeeSpecies[] { BeeSpecies.FOREST, BeeSpecies.MEADOWS} }
        };

        public static BeeSpecies[] AccesDictionary(BeeSpecies[] key)
        {
            try
            {
                return Possiblechildren[SortArray(key)];
            }
            catch
            {
                //is their is not corrisponding key return missing 
                return new BeeSpecies[] { BeeSpecies.MISSING };
            }
        }

        private static BeeSpecies[] SortArray(BeeSpecies[] unsortedArray)
        {
            //makes variables that are used for the sorting
            int[] intArray = new int[unsortedArray.Length];
            BeeSpecies[] sortedArray = new BeeSpecies[unsortedArray.Length];

            //comvertes all of the enums to int so can be easily sorted
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                intArray[i] = unsortedArray[i].GetHashCode();
            }

            Array.Sort(intArray);

            //reconverts the int back to enums
            for (int i = 0; i < intArray.Length; i++)
            {
                sortedArray[i] = (BeeSpecies)intArray[i];
            }

            //returns the sorted array
            return sortedArray;
        }
    }
}