using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dandelion : Plant
{
    [SerializeField] private Plant prefab;


    public override void OnPlant()
    {

    }

    public override void Harvest()
    {
        int original = plot.pos.x;
        int row = plot.pos.y;
        for (int i = 0; i < plot.garden.gardenWidth; i++)
        {
            if (i != original && !plot.garden.allPlots[row][i].plant)
            {
                Dandelion copy = (Dandelion) plot.garden.allPlots[row][i].addPlant(prefab, true);
                copy.maxTurns = maxTurns;
                copy.currentTurns = 0;
            }
        }
        base.Harvest();
    }

}
