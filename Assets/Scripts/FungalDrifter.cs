using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungalDrifter : Plant
{
    [SerializeField] private Plant prefab;
    bool moved = true;
    // Start is called before the first frame update
    public override void Grow()
    {
        if (!moved)
        {
            var emptyAdj = plot.garden.getAdjacent(new CoordPair(plot.pos.x, plot.pos.y), false);
            if (emptyAdj.Count > 0)
            {
                var mCoords = emptyAdj[Random.Range(0, emptyAdj.Count)];
                var p = plot.garden.allPlots[mCoords.y][mCoords.x].addPlant(prefab, false);
                p.name = prefab.gameObject.name;
                DestroyPlant();
            }
            base.Grow();
        }
        else
        {

        }
    }

    public override IEnumerator TurnStart()
    {
        moved = false;
        yield return null;
    }
    public override void OnPlant()
    {
        moved = false;
    }
}
