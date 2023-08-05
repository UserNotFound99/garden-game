using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thornweed : Plant
{
    [SerializeField] private Plant prefab;
    private int copies = 4;
    public override void OnPlant()
    {
        List<CoordPair> addable = plot.garden.getAdjacent(plot.pos, false);

        for (int i = 0; i < copies - 1; i++)
        {
            CoordPair randomPair = plot.pos;
            while (plot.garden.allPlots[randomPair.y][randomPair.x].plant)
            {
                if (addable.Count == 0) return;

                randomPair = addable[Random.Range(0, addable.Count)];
                addable.Remove(randomPair);
            }
            plot.garden.allPlots[randomPair.y][randomPair.x].addPlant(prefab, false);
            addable.AddRange(plot.garden.getAdjacent(randomPair, false));
        }
    }
}
