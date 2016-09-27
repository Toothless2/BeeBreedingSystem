using UnityEngine;
using System.Collections.Generic;
using System;

namespace Bee
{
    public class CombinableDictionary
    {
        private static Dictionary<EnumBeeSpecies[], EnumBeeSpecies[]> Possiblechildren = new Dictionary<EnumBeeSpecies[], EnumBeeSpecies[]>(new EqualityComparer())
        {
            {SortArray(new EnumBeeSpecies[] {EnumBeeSpecies.FOREST, EnumBeeSpecies.FOREST}), new EnumBeeSpecies[] { EnumBeeSpecies.FOREST} },
            {SortArray(new EnumBeeSpecies[] {EnumBeeSpecies.FOREST, EnumBeeSpecies.MEADOWS}), new EnumBeeSpecies[] {EnumBeeSpecies.FOREST, EnumBeeSpecies.MEADOWS} }
        };

        public static EnumBeeSpecies[] AccesDictionary(EnumBeeSpecies[] key)
        {
            try
            {
                return Possiblechildren[SortArray(key)];
            }
            catch
            {
                //is their is not corrisponding key return missing 
                return new EnumBeeSpecies[] { EnumBeeSpecies.MISSING };
            }
        }

        private static EnumBeeSpecies[] SortArray(EnumBeeSpecies[] unsortedArray)
        {
            //makes variables that are used for the sorting
            int[] intArray = new int[unsortedArray.Length];
            EnumBeeSpecies[] sortedArray = new EnumBeeSpecies[unsortedArray.Length];

            //comvertes all of the enums to int so can be easily sorted
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                intArray[i] = unsortedArray[i].GetHashCode();
            }

            Array.Sort(intArray);

            //reconverts the int back to enums
            for (int i = 0; i < intArray.Length; i++)
            {
                sortedArray[i] = (EnumBeeSpecies)intArray[i];
            }

            //returns the sorted array
            return sortedArray;
        }
    }
}