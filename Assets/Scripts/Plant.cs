using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int maxTurns = 3;
    public int currentTurns = 0;
    public int reward = 3;

    public bool destructible = true;

    [System.NonSerialized] public bool ready = false;

    public GameObject infoPrefab;
    [System.NonSerialized] public GameObject info = null;

    [System.NonSerialized] public Plot plot;

    [System.NonSerialized] private SpriteRenderer plantSprite;

    [SerializeField] public Sprite icon;

    [SerializeField] public List<Sprite> allSprites;

    [SerializeField] protected List<Effect> allEffects;



    [System.NonSerialized] public TextMeshPro turnsLeft;

    [System.NonSerialized] public ProgressBar progressBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (turnsLeft) turnsLeft.text = maxTurns.ToString();
        while (allSprites.Count < (maxTurns+1) % 999) {
            allSprites.Add(allSprites[allSprites.Count - 1]);
        }
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
        foreach (Effect e in allEffects) e.TurnStart();
    }
    public virtual void OnPlant()
    {

    }
    public virtual void Grow()
    {
        for (int i = 0; i < plot.calcGrow() * getEffectProduct(); i++)
        {
            foreach (Effect e in allEffects) e.OnGrow();
            if (maxTurns < 999)
            {
                GrowOnce();
            }
            else
            {
                plot.garden.addCoins(reward);
            }
        }
    }

    protected void GrowOnce()
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

    private int getEffectProduct()
    {
        float x = 1;
        /*
        for (int i = 0; i < allEffects.Count; i++)
        {
            x += allEffects[i].bonusSpeed;
        }*/
        return Mathf.RoundToInt(x);
    }
    public virtual void AddEffect(Effect e)
    {
        allEffects.Add(e);
        e.setPlant(this);
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
        foreach (Effect e in allEffects) e.OnHarvest();
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
