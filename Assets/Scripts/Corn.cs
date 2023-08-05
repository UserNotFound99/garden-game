using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : Plant
{
    [SerializeField] int rowReward;
    public override void Harvest()
    {
        
        bool fullRow = true;
        for (int i = 0; i < plot.garden.gardenWidth; i++)
        {
            if (!plot.garden.allPlots[plot.pos.y][i].plant)
            {
                fullRow = false;
            }
        }
        if (fullRow) reward += rowReward;
        base.Harvest();
    }
}
