using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    protected Plant attachedPlant;
    public virtual void OnHarvest()
    {

    }

    public virtual void OnGrow()
    {

    }

    public virtual void TurnStart()
    {

    }

    public void setPlant(Plant p)
    {
        attachedPlant = p;
    }
}
