using UnityEngine;
using System.Collections.Generic;
using Bees.Genetics.Enums;

namespace Bees.Genetics.Breeding
{
    public class BeeBreeder : MonoBehaviour
    {
        public List<Bee> inputbees;
        public List<Bee> outputbee;

        void Start()
        {
            if(inputbees.Count > 0)
            {
                BeeSpecies[] temp = SpeciesCombiner.CombineSpecies(inputbees[0].Species, inputbees[1].Species);

                for (int i = 0; i < temp.Length; i++)
                {
                    Bee bee = new Bee(temp[i], TempratureTolarence.NORMAL);
                    outputbee.Add(bee);
                }
            }
        }
    }
}