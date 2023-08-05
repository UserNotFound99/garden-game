using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : StoreBox
{
    public int itemReference;

    public override void ButtonPress()
    {
        if (!checkPrice()) return;
        base.ButtonPress();
        garden.addItem(itemReference);
    }
}
