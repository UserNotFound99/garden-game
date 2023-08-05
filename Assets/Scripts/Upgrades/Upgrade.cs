using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [System.NonSerialized] public int id;
    protected EventTab tab;

    private void Start()
    {
        tab = FindObjectOfType<EventTab>();
    }
    private void OnMouseUpAsButton()
    {
        if (tab.result < 0) tab.result = id;
        Use();
    }
    public virtual void Use()
    {
        print("Upgrade was used!");
    }
}
