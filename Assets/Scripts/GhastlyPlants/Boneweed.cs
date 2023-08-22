using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boneweed : GhastlyPlant
{
    [SerializeField] Plant boneBlock;
    public override void Harvest()
    {
        if (!exorcised)
        {
            plot.plant = null;
            plot.addPlant(boneBlock);
        }
        base.Harvest();
    }
}
