using UnityEngine;
using System;
using Bees.Genetics.Enums;

namespace Bees.Genetics
{
    public interface BeeStats
    {
        BeeSpecies Species
        {
            get;
            set;
        }

        TempratureTolarence TemptratureTolerance
        {
            get;
            set;
        }
    }
}