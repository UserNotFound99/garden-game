using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exorcitrus : GhastlyPlant
{
    public override void Harvest()
    {
        var neighbors = plot.garden.getNeighbors(new CoordPair(plot.pos.x, plot.pos.y), true);
        foreach (CoordPair p in neighbors) {
            var neighborPlant = plot.garden.allPlots[p.y][p.x].plant;
            var ghastlyPlant = neighborPlant ? neighborPlant.GetComponent<GhastlyPlant>() : null;
            if (ghastlyPlant != null)  ghastlyPlant.Exorcise();  
        }
        base.Harvest();
    }
}
