using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class SubjectZero : Plant
{
    [SerializeField] int seasonNum;
    public override void AddEffect(Effect e)
    {
        base.AddEffect(e);
        GrowOnce();
    }
    public override void Grow()
    {
        foreach (Effect e in allEffects) e.OnGrow();
    }

    public override void OnPlant()
    {
        plot.garden.setList(seasonNum, true);
    }

    public void OnDestroy()
    {

        plot.garden.setList(seasonNum, false);
    }
}
