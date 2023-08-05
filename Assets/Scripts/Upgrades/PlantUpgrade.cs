using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUpgrade : Upgrade
{
    public List<Plant> plants;
    public int plantNum = 1;
    public bool planted = false;

    public override void Use()
    {
        for (int i = 0; i < plantNum; i++)
        {
            CoordPair c = tab.garden.getRandomPlot();
            if (c.x != -1) tab.garden.allPlots[c.x][c.y].addPlant(plants[Random.Range(0, plants.Count)]);
            else return;
            if (planted) tab.garden.numPlanted = 1;
        }
    }
}
