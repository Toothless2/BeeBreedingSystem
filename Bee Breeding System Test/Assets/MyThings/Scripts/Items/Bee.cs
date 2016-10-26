using Bees.Entity;
using Bees.Genetics.Enums;

namespace Bees
{
    [System.Serializable]
    public class Bee : BeeEntity
    {
        public BeeSpecies species;
        public TempratureTolarence temp;
        
        [System.Obsolete("Do not use cases errors")]
        public Bee(BeeSpecies _species, TempratureTolarence _tolerance) : base(_species, _tolerance)
        {
            species = _species;
            temp = _tolerance;
        }
        
        //makes it easier to assign the variables from another script
        public void AssignVariables(int? _slotindex, BeeSpecies _species, TempratureTolarence _temp)
        {
            slotindex = _slotindex;
            species = _species;
            temp = _temp;
        }
    }
}