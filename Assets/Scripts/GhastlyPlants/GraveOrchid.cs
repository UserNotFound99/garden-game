using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraveOrchid : GhastlyPlant
{
    int gIndex;

    protected override void Start()
    {
        base.Start();
        Garden garden = plot.garden;
        gIndex = garden.market.getIndexOfList("GhastlyPlants");
        if (gIndex == -1) return;
        garden.market.stagePlants[gIndex].multiplier += 1;

    }

    private void OnDestroy()
    {
        if (gIndex == -1) return;
        plot.garden.market.stagePlants[gIndex].multiplier -= 1;
    }
}
