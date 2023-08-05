using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : Upgrade
{
    public int itemNum = 0;
    public int itemQty = 3;

    public override void Use()
    {
        tab.garden.addItem(itemNum, itemQty);
    }
}
