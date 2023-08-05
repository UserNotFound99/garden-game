using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowUpgrade : Upgrade
{
    public int growTimes = 5;

    public override void Use()
    {
        for (int i = 0; i < growTimes; i++)
        {
            for (int y = 0; y < tab.garden.gardenHeight; y++)
            {
                for (int x = 0; x < tab.garden.gardenWidth; x++)
                {
                    tab.garden.allPlots[y][x].GrowPlant();
                }
            }
        }
    }
}
