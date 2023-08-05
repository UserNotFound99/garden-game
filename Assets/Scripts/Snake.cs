using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Plant
{
    int lockedSlot = -1;

    public override void OnPlant()
    {
        lockedSlot = plot.garden.market.lastSlot;
        plot.garden.market.lockSlot(lockedSlot);
    }

    public void OnDestroy()
    {
        if (lockedSlot >= 0 && plot) plot.garden.market.unlockSlot(lockedSlot);
    }
}
