using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;

public class BoxAnimation : MonoBehaviour
{
    Garden garden;
    Plant plant;
    SpriteRenderer spriteRenderer;
    [SerializeField] float countdown;
    private float thresh;
    void Start()
    {
        garden = FindObjectOfType<Garden>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        garden.actionsAllowed = false;
        thresh = countdown / 4;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            garden.actionsAllowed = true;
            CoordPair c = garden.getRandomPlot();
            if (c != null && c.x >= 0) garden.allPlots[c.y][c.x].addPlant(plant);
            Destroy(gameObject);
        }
        else if (countdown <= thresh)
        {
            spriteRenderer.sprite = plant.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void SetPlant(Plant p)
    {
        plant = p;
    }
}
