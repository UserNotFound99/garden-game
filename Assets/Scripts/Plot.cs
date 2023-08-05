using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour
{
    
    [System.NonSerialized] public Garden garden;
    [System.NonSerialized] public Plant plant;
    [System.NonSerialized] public CoordPair pos;
    public BoxCollider2D plantBox;
    public List<PlotMod> mods;
    private bool mouseOn;
    private float onTimer = 0.0f, onMax = 0.5f;


    private SpriteRenderer plotRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        plotRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        if (!garden.actionsAllowed) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!plant)
            {
                var plantedPlant = garden.market.GetPlant();
                if (!plantedPlant) return;
                addPlant(plantedPlant);
                garden.numPlanted++;
            }
            else
            {
                if (plant.ready) plant.Harvest();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (garden.shovelsLeft > 0)
            {
                plant.DestroyPlant();
                //garden.addCoins(-garden.shovelPrice);
                garden.shovelsLeft -= 1;
                garden.shovelText.text = garden.shovelsLeft.ToString();
            }
        }
    }

    public void OnMouseEnter()
    {
        mouseOn = true;
    }

    public void OnMouseExit()
    {
        mouseOn = false;
        onTimer = 0;
        if (plant) plant.Hover(false);
    }

    public Plant addPlant(Plant p, bool onPlant = true)
    {
        plant = Instantiate(p, transform.position, Quaternion.identity);
        plant.plot = this;
        if (onPlant)
        {
            plant.OnPlant();
            garden.playPlantSound();
        }
        return plant;

    }

    public PlotMod addMod(PlotMod m)
    {
        PlotMod mod = Instantiate(m, transform.position, Quaternion.identity);
        mod.transform.parent = transform;
        mods.Add(mod);
        return mod;
    }

    public void StartTurn()
    {
        if (plant)
            plant.TurnStart();
    }
    public void GrowPlant()
    {
        if (plant) 
            plant.Grow();
    }

    public int calcReward(int baseReward)
    {
        float x = baseReward;
        for (int i = 0; i < mods.Count; i++)
        {
            x += mods[i].added;
        }
        for (int i = 0; i < mods.Count; i++)
        {
            x *= mods[i].multiplier;
        }
        for (int i = 0; i < mods.Count; i++)
        {
            if (mods[i].uses == 99) continue;
            if (--mods[i].uses <= 0)
            {
                Destroy(mods[i].gameObject);
            }
        }
        return Mathf.RoundToInt((garden.addReward + x) * garden.multReward);
    }

    public int calcGrow()
    {
        float x = 1;
        for (int i = 0; i < mods.Count; i++)
        {
            x *= mods[i].growSpeed;
        }
        return Mathf.RoundToInt(x);
    }

    public void SetPlot(bool enabled)
    {
        plotRenderer.enabled = enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOn && onTimer < onMax)
        {
            onTimer += Time.deltaTime;
            if (onTimer >= onMax && plant) plant.Hover(true);
        }
    }
}
