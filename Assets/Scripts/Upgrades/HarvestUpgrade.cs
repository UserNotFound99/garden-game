using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestUpgrade : Upgrade
{
    public int newAdd = 0;
    public int newMult = 1;

    public override void Use()
    {
        tab.garden.addReward = newAdd;
        tab.garden.multReward = newMult;
    }
}
