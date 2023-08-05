using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : StoreBox
{
    [SerializeField] BoxAnimation boxAnimation;
    [SerializeField] List<Plant> plants;

    public override void ButtonPress()
    {
        if (!checkPrice()) return;
        base.ButtonPress();
    }

    public override void PressMany()
    {
        BoxAnimation b = Instantiate(boxAnimation, garden.transform);
        b.SetPlant(plants[Random.Range(0, plants.Count)]);
    }
}
