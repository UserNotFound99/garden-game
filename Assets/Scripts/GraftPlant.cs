using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraftPlant : Plant
{
    [SerializeField] Effect graftEffect;
    public override void OnPlant()
    {
        base.OnPlant();
        AddEffect(graftEffect);
    }

    public Effect getEffect()
    {
        return graftEffect;
    }
}
