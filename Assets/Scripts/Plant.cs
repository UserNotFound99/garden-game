using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int maxTurns = 3;
    public int currentTurns = 0;
    public int reward = 3;

    public bool ready = false;

    public GameObject infoPrefab;
    [System.NonSerialized] public GameObject info = null;

    [System.NonSerialized] public Plot plot;

    [System.NonSerialized] private SpriteRenderer plantSprite;

    [SerializeField] public Sprite icon;

    [SerializeField] public List<Sprite> allSprites;

    



    [System.NonSerialized] public TextMeshPro turnsLeft;

    [System.NonSerialized] public ProgressBar progressBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (turnsLeft) turnsLeft.text = maxTurns.ToString();
        if (allSprites.Count != (maxTurns + 1) % 999) print("Incorrect sprite count!");
        plantSprite = gameObject.GetComponent<SpriteRenderer>();
        plantSprite.sprite = allSprites[0];
        turnsLeft = GetComponentInChildren<TextMeshPro>();
        turnsLeft.enabled = false;
        progressBar = GetComponentInChildren<ProgressBar>(true);
        progressBar.Disappear();

    }

    public virtual void TurnStart()
    {

    }
    public virtual void OnPlant()
    {

    }

    public virtual void Grow()
    {
        for (int i = 0; i < plot.calcGrow(); i++)
        {
            if (maxTurns < 999)
            {
                if (currentTurns >= maxTurns) return;
                plantSprite.sprite = allSprites[++currentTurns];
                progressBar.SetFraction(currentTurns, maxTurns);
                if (turnsLeft) turnsLeft.text = (maxTurns - currentTurns).ToString();
                if (Done())
                {
                    ReadyHarvest();
                }
            }
            else
            {
                plot.garden.addCoins(reward);
            }
        }
    }

    public virtual bool Done()
    {
        return currentTurns == maxTurns;
    }

    public virtual void ReadyHarvest()
    {
        ready = true;
    }

    public virtual void Harvest()
    {
        plot.garden.addCoins(plot.calcReward(reward));
        DestroyPlant();
    }

    public void DestroyPlant()
    {
        Destroy(gameObject);
    }

    public void Hover(bool hovered)
    {
        StartCoroutine(hovered ? OnHovered() : OffHovered());
    }

    private IEnumerator OnHovered()
    {
        progressBar.Appear();
        yield return null;
    }

    private IEnumerator OffHovered()
    {
        //yield return new WaitForSeconds(1f);
        progressBar.Disappear();
        yield return null; 
    }
}
