using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CoordPair
{
    public int x, y;
    public CoordPair(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

public class Garden : MonoBehaviour
{
    public int gardenWidth = 6, gardenHeight = 5;
    [SerializeField] private GameObject plotPrefab;

    [System.NonSerialized] public Plant selectedPlant;
    [System.NonSerialized] public int coins = 0;
    [System.NonSerialized] public int corpses = 0;

    [System.NonSerialized] public int numPlanted = 0;

    public List<List<Plot>> allPlots = new List<List<Plot>>();

    [System.NonSerialized] public Market market;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI corpseText;

    [Header("Sound")]
    [SerializeField] private AudioSource SFXPlayer;
    [SerializeField] private AudioClip plantClip;

    [Header("Turns")]
    public TextMeshProUGUI turnCount;
    private int turnNum = 1;


    [Header("Shovel")]
    public TextMeshProUGUI shovelText;
    public int shovelsLeft = 0;

    [Header("Reroll")]
    public TextMeshProUGUI rerollText;
    public int rerollsLeft = 0;

    [Header("Plant Lists")]
    public List<PlantList> lists;
    private List<bool> activeSeasons;
    private List<string> seasonNames = new List<string>() {
        "Garden",
        "Ghastly"
    };


    [System.NonSerialized] public List<Store> allStores = new List<Store>(new Store[3]);
    [System.NonSerialized] public EventController eventController;
    [System.NonSerialized] public bool actionsAllowed = true;

    [System.NonSerialized] public int addReward = 0;
    [System.NonSerialized] public float multReward = 1;

    // Start is called before the first frame update
    void Start()
    {
        market = FindObjectOfType<Market>();
        eventController = GetComponent<EventController>();
        shovelText.text = shovelsLeft.ToString();
        activeSeasons = new List<bool>(new bool[lists.Count()]);

        //var plotSize = plotPrefab.GetComponent<SpriteRenderer>().bounds.size;
        for (int y = 0; y < gardenHeight; y++)
        {
            List<Plot> plots = new List<Plot>();
            for (int x = 0; x < gardenWidth; x++)
            {
                Plot newPlot = transform.Find("Plot (" + x + ", " + y + ")").GetComponent<Plot>();
                newPlot.pos = new CoordPair(x, y);
                newPlot.garden = this;
                plots.Add(newPlot);
            }
            allPlots.Add(plots);
        }
    }

    private bool inBounds(CoordPair coords){
        return (coords.x >= 0 && coords.x < gardenWidth && coords.y >= 0 && coords.y < gardenHeight);
    }

    public List<CoordPair> getNeighbors(CoordPair coords, bool occupied, bool destructibleOnly = false)
    {
        List<CoordPair> possible = new List<CoordPair>
        {
            new CoordPair(coords.x, coords.y + 1),
            new CoordPair(coords.x, coords.y - 1),
            new CoordPair(coords.x + 1, coords.y),
            new CoordPair(coords.x + 1, coords.y + 1),
            new CoordPair(coords.x + 1, coords.y - 1),
            new CoordPair(coords.x - 1, coords.y),
            new CoordPair(coords.x - 1, coords.y + 1),
            new CoordPair(coords.x - 1, coords.y - 1)
        };
        int i = 0;
        while (i < possible.Count)
        {
            if (!inBounds(possible[i]))
            {
                possible.Remove(possible[i]);
            }
            else if (occupied == !allPlots[possible[i].y][possible[i].x].plant)
            {
                possible.Remove(possible[i]);
            }
            else if (occupied && destructibleOnly && !allPlots[possible[i].y][possible[i].x].plant.destructible)
            {
                possible.Remove(possible[i]);
            }
            else
            {
                ++i;
            }
        }

        return possible;
    }

    public List<CoordPair> getAdjacent(CoordPair coords, bool occupied, bool destructibleOnly = false)
    {
        List<CoordPair> possible = new List<CoordPair>
        {
            new CoordPair(coords.x, coords.y + 1),
            new CoordPair(coords.x, coords.y - 1),
            new CoordPair(coords.x + 1, coords.y),
            new CoordPair(coords.x - 1, coords.y)
        };
        int i = 0;
        while (i < possible.Count)
        {
            if (!inBounds(possible[i]))
            {
                possible.Remove(possible[i]);
            }
            else if (occupied == !allPlots[possible[i].y][possible[i].x].plant)
            {
                possible.Remove(possible[i]);
            }
            else if (occupied && destructibleOnly && !allPlots[possible[i].y][possible[i].x].plant.destructible)
            {
                possible.Remove(possible[i]);
            }
            else
            {
                ++i;
            }
        }

        return possible;
    }

    public void playPlantSound()
    {
        SFXPlayer.PlayOneShot(plantClip);
    }

    public CoordPair getRandomPlot(int row = -1, int col = -1, int hasPlant = 0, int hasMod = -1)
    {
        List<CoordPair> possible = new List<CoordPair>();
        for (int i = 0; i < allPlots.Count; ++i)
        {
            for (int j = 0; j < allPlots[i].Count; ++j)
            {
                if (row != -1 && row != i) continue;
                if (col != -1 && col != j) continue;
                if (hasMod == 0 && allPlots[i][j].mods.Count > 0) continue;
                if (hasMod == 1 && allPlots[i][j].mods.Count == 0) continue;
                if ((bool) allPlots[i][j].plant == (hasPlant == 1) || hasPlant == -1) possible.Add(new CoordPair(i, j));
            }
        }
        if (possible.Count > 0)
        {
            return possible[Random.Range(0, possible.Count)];
        }
        return new CoordPair(-1, -1);
    }

    public void addCoins (int _coins)
    {
        coins += _coins;
        goldText.text = "$" + coins.ToString();
    }

    public void addCorpses (int _corpses)
    {
        corpses += _corpses;
        corpseText.transform.parent.gameObject.SetActive(corpses > 0);
        if (corpses <= 0)
        {
            setList(findListIndex("Ghastly"), corpses > 0);
            print("The dead wither away once more...");
        }
        corpseText.text = corpses.ToString();
    }

    public void addItem(int index, int num = 1)
    {
        switch (index)
        {
            case 0:
                shovelsLeft += num;
                shovelText.text = shovelsLeft.ToString();
                break;
            case 1:
                rerollsLeft += num;
                rerollText.text = rerollsLeft.ToString();
                break;
        }
    }

    public void setList(int index, bool on)
    {
        if (index == 0) return;
        activeSeasons[index] = on;
        int searchResult = market.getIndexOfList(lists[index].name);
        if (on && searchResult < 0) market.addPlantList(lists[index]);
        else if (!on && searchResult >= 0) market.removePlantList(searchResult);
    }

    public int findListIndex(string name)
    {
        return seasonNames.FindIndex(str => str==name);
    }

    //activates turn end, then harvests & refreshes market
    public IEnumerator endTurn()
    {
        numPlanted = 0;
        addReward = 0;
        multReward = 1;

        for (int y = 0; y < gardenHeight; y++)
        {
            for (int x = 0; x < gardenWidth; x++)
            {
                allPlots[y][x].GrowPlant();
            }
        }

        yield return new WaitForSeconds(0.05f);

        for (int y = 0; y < gardenHeight; y++)
        {
            for (int x = 0; x < gardenWidth; x++)
            {
                if (allPlots[y][x].plant && allPlots[y][x].plant.Done())
                {
                    allPlots[y][x].plant.ReadyHarvest();
                }
            }
        }
        turnNum++;
        turnCount.text = "Day " + turnNum.ToString();
        StartCoroutine(eventController.proceed(turnNum));
    }

    bool checkOver()
    {
        if (shovelsLeft > 0 || allStores[0].price <= coins) return false;
        if (market.allLocked()) return true;
        for (int y = 0; y < gardenHeight; y++)
        {
            for (int x = 0; x < gardenWidth; x++)
            {
                if (!allPlots[y][x].plant)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void startTurn()
    {
        for (int y = 0; y < gardenHeight; y++)
        {
            for (int x = 0; x < gardenWidth; x++)
            {
                allPlots[y][x].StartTurn();
            }
        }
    }

    IEnumerator reroll()
    {
        market.Clear();
        yield return new WaitForSeconds(0.05f);
        market.AddPackets();
        rerollsLeft--;
        rerollText.text = rerollsLeft.ToString();
    }

    IEnumerator nextTurn()
    {
        StartCoroutine(endTurn());
        yield return new WaitForSeconds(0.1f);
        market.AddPackets();
        startTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && actionsAllowed)
        {
            if (numPlanted > 0)
            {
                StartCoroutine(nextTurn());
            }
            else if (rerollsLeft > 0)
            {
                StartCoroutine(reroll());
            }
        }
    }
}
