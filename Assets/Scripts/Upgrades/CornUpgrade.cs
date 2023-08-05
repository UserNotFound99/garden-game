using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornUpgrade : Upgrade
{
    public Plant plant;

    public override void Use()
    {
        for (int i = 0; i < tab.garden.allPlots.Count; i++)
        {
            CoordPair c = tab.garden.getRandomPlot(i);
            if (c.x != -1) tab.garden.allPlots[c.x][c.y].addPlant(plant);
        }
    }
}
