using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeasonSwitcher : MonoBehaviour
{
    //negative: take away; positive: add a season
    Garden garden;
    [SerializeField] int startSwitch;
    [SerializeField] int endSwitch;

    void Awake()
    {
        garden = FindObjectOfType<Garden>();
        garden.setList(Mathf.Abs(startSwitch), (startSwitch > 0));
    }

    private void OnDestroy()
    {
        garden.setList(Mathf.Abs(endSwitch), (endSwitch > 0));
    }
}
