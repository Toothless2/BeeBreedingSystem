using UnityEngine;
using Bees.Genetics.Enums;
using Bees.Genetics;
using Items;

namespace Bees.Entity
{
    [System.Serializable]
    public class BeeEntity : Item, BeeStats
    {
        private BeeSpecies species;
        private TempratureTolarence temptolerance;
        
        public BeeEntity(BeeSpecies _species, TempratureTolarence _tolerance)
        {
            species = _species;
            temptolerance = _tolerance;
        }

        public BeeSpecies Species
        {
            get
            {
                return species;
            }

            set
            {
                species = value;
            }
        }

        public TempratureTolarence TemptratureTolerance
        {
            get
            {
                return temptolerance;
            }

            set
            {
                temptolerance = value;
            }
        }
    }
}