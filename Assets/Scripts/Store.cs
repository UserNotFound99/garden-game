using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    private Garden garden;
    public TextMeshProUGUI text;
    public int price;
    public int increment;
    public int itemNum;
    // Start is called before the first frame update
    void Start()
    {
        garden = FindObjectOfType<Garden>();
        garden.allStores[itemNum] = this;
        text.text = "$" + price.ToString();
    }

    public void Buy()
    {
        if (garden.coins >= price)
        {
            garden.addCoins(-price);
            garden.addItem(itemNum);
            price += increment;
            text.text = "$" + price.ToString();
        }
    }
}
