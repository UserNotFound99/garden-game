using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantBox : StoreBox
{
    public Packet packet;
    public int odds = 3;
    public bool bought = false;
    public bool on = false;

    public override void ButtonPress()
    {
        if (!bought)
        {
            if (!checkPrice()) return;
            base.ButtonPress();
            bought = true;
        }
        Toggle();
    }

    public override void PressOnce()
    {
        buttonText.text = "Enabled";
        costText.text = "Owned";
    }
    
    public void Toggle()
    {
        if (on)
        {
            on = false;
            garden.market.addedPlants.AddPacket(packet, -odds);
            buttonText.text = "Disabled";
        }
        else
        {
            on = true;
            garden.market.addedPlants.AddPacket(packet, odds);
            buttonText.text = "Enabled";
        }
    }
}
