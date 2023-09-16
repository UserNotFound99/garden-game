using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FertilizerUpgrade : Upgrade
{
    public PlotMod mod;
    public int num = 3;
    public override void Use()
    {
        for (int i = 0; i < num; i++)
        {
            CoordPair c = tab.garden.getRandomPlot(hasPlant:-1, hasMod: 0);
            if (c.x == -1) return;
            Plot p = tab.garden.allPlots[c.y][c.x];
            tab.garden.allPlots[c.y][c.x].addMod(mod);
        }
    }
}
