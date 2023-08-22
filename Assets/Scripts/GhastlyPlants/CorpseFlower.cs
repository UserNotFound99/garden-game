using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseFlower : Plant
{
    //negative: take away; positive: add a season
    [SerializeField] int seasonNum;
    [SerializeField] int initialCorpses;
    [SerializeField] string message;

    public override void OnPlant()
    {
        plot.garden.setList(seasonNum, true);
        plot.garden.addCorpses(initialCorpses);
        if (message != null) Debug.Log(message);
    }

    private void OnDestroy()
    {
        plot.garden.setList(seasonNum, true);
    }
}