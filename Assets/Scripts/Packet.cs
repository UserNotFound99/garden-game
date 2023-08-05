using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour
{
    public Plant plant;
    [System.NonSerialized] public Market market;
    [System.NonSerialized] public int slotNum;
    Vector2 infoDim;
    private bool mouseOn = false;
    private float onTimer = 0.0f, onMax = 0.5f;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            market.selectPlant(slotNum);
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
        ShowInfo(false);
    }

    public void ShowInfo(bool enabled)
    {
        if (!plant.info)
        {
            plant.info = Instantiate(plant.infoPrefab, Vector2.zero, Quaternion.identity);
            //plant.info.transform.parent = transform;
            //infoDim = plant.info.transform.GetComponent<SpriteRenderer>().bounds.size;
            
        }
        plant.info.transform.position = new Vector2(market.infoX,
                Mathf.Clamp(transform.position.y, market.infoYMin, market.infoYMax));
        if (enabled)  plant.info.SetActive(true);
        else plant.info.SetActive(false);
    }

    private void Update()
    {
        if (mouseOn && onTimer < onMax && market.garden.actionsAllowed)
        {
            onTimer += Time.deltaTime;
            if (onTimer >= onMax) ShowInfo(true);
        }
    }
}
