using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreBox : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Button buyButton;
    [SerializeField] int cost;
    [SerializeField] int ramp;
    [SerializeField] bool repeatable;

    protected TextMeshProUGUI costText;
    protected TextMeshProUGUI buttonText;

    protected Garden garden;

    private void Start()
    {
        garden = FindObjectOfType<Garden>();
        costText = icon.GetComponentInChildren<TextMeshProUGUI>();
        costText.text = ("$" + cost.ToString());
        buttonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected bool checkPrice()
    {
        return garden.coins >= cost;
    }
    public virtual void ButtonPress() 
    {
        if (garden.coins < cost) return;
        garden.addCoins(-cost);
        if (!repeatable)
        {
            PressOnce();
        }
        else
        {
            PressMany();
            cost += ramp;
            costText.text = ("$" + cost.ToString());
        }
    }

    public virtual void PressOnce()
    {
        buyButton.gameObject.SetActive(false);
        costText.enabled = false;
    }

    public virtual void PressMany()
    {

    }
}
