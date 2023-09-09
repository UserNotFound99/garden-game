using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraftEffect : Effect
{
    [SerializeField] int pointsPerRound = 0;

    public override void TurnStart()
    {
        attachedPlant.plot.garden.addCoins(pointsPerRound);
        base.TurnStart();
    }
}
