using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AshUpgrade : Upgrade
{
    public PlotMod mod;

    public override void Use()
    {
        CoordPair c = tab.garden.getRandomPlot(hasPlant:1, hasMod:0);
        Plot p = tab.garden.allPlots[c.x][c.y];
        if (c.x == -1) return;
        p.plant.DestroyPlant();
        tab.garden.allPlots[c.x][c.y].addMod(mod);
    }
}
