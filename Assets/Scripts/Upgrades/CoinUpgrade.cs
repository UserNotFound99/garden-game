using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUpgrade : Upgrade
{
    public int coinNum = 10;

    public override void Use()
    {
        tab.garden.addCoins(coinNum);
    }
}
