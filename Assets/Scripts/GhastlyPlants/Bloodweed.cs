using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodweed : GhastlyPlant
{
    public override void Harvest()
    {
        var neighbors = plot.garden.getAdjacent(new CoordPair(plot.pos.x, plot.pos.y), true);
        foreach (CoordPair p in neighbors)
        {
            var neighborPlant = plot.garden.allPlots[p.y][p.x].plant;
            var ghastlyPlant = neighborPlant ? neighborPlant.GetComponent<GhastlyPlant>() : null;
            if (ghastlyPlant == null || !ghastlyPlant.getUndead()) neighborPlant.DestroyPlant();
        }
        base.Harvest();
    }
}
