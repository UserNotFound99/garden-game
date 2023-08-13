using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightshade : Plant
{
    public override void Harvest()
    {
        var neighbors = plot.garden.getNeighbors(new CoordPair(plot.pos.x, plot.pos.y), true);
        if (neighbors.Count > 0)
        {
            var destroyCoords = neighbors[Random.Range(0, neighbors.Count)];
            plot.garden.allPlots[destroyCoords.y][destroyCoords.x].plant.DestroyPlant();
        }
        base.Harvest();
    }
}
