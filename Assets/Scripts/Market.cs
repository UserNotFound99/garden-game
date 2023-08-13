using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public struct FuturePacket
{
    public int turns;
    public Packet packet;
    public FuturePacket(int _turns, Packet _packet)
    {
        turns = _turns;
        packet = _packet;
    }
}

public class Market : MonoBehaviour
{
    [SerializeField] private int slots = 3;
    [SerializeField] private GameObject selectorPrefab;
    [SerializeField] private GameObject lockerPrefab;
    public List<PlantList> stagePlants;
    public PlantList addedPlants;
    public List<FuturePacket> qPackets;
    public float infoX, infoYMin, infoYMax;
    [System.NonSerialized] public Garden garden;
    private List<Packet> cMarket;
    private List<GameObject> lockers;
    private float xCoord;
    private List<float> yCoords;
    private GameObject selector;

    private int selectedSlot = -1;
    public int lastSlot = -1;

    // Start is called before the first frame update
    void Start()
    {
        garden = FindObjectOfType<Garden>();
        foreach (PlantList pl in stagePlants) pl.ResetList();
        addedPlants = PlantList.CreateInstance<PlantList>();
        qPackets = new List<FuturePacket>();
        cMarket = new List<Packet>(new Packet[slots]);
        lockers = new List<GameObject>();
        var marketSize = GetComponent<SpriteRenderer>().bounds.size;
        xCoord = transform.position.x;
        yCoords = new List<float>();
        selector =  Instantiate(selectorPrefab, new Vector3(xCoord, 0), Quaternion.identity, transform);
        selector.SetActive(false);
        for (int y = 0; y < slots; y++)
        {
            yCoords.Add(transform.position.y + marketSize.y / (float)slots * ((float)y + 0.5f) - marketSize.y * 0.5f);
            lockers.Add(Instantiate(lockerPrefab, new Vector3(xCoord, yCoords[y]), Quaternion.identity));
            lockers[y].transform.parent = transform;
            unlockSlot(y);
        }
        AddPackets();
    }
    

    public void AddPackets()
    {
        int numMissing = 0;
        for (int i = 0; i < slots; i++) if (!cMarket[i]) numMissing++;

        List<Packet> mustPackets = new List<Packet>();
        for(int i = qPackets.Count - 1; i >= 0; --i){
            qPackets[i] = new FuturePacket(qPackets[i].turns -1 , qPackets[i].packet);
            if (qPackets[i].turns <= 0)
            {
                mustPackets.Add(qPackets[i].packet);
                qPackets.RemoveAt(i);
            }
            if (mustPackets.Count >= numMissing) break;
        }
        

        for (int i = 0; i < slots; i++)
        {
            if (!cMarket[i])
            {
                if (mustPackets.Count > 0)
                {
                    int usedPacket = Random.Range(0, mustPackets.Count);
                    cMarket[i] = Instantiate(mustPackets[usedPacket], new Vector3(xCoord, yCoords[i]), Quaternion.identity);
                    mustPackets.RemoveAt(usedPacket);
                }
                else
                {
                    cMarket[i] = Instantiate(GetNewPacket(), new Vector3(xCoord, yCoords[i]), Quaternion.identity);
                }
                cMarket[i].transform.parent = transform;
                cMarket[i].market = this;
                cMarket[i].slotNum = i;
            }
        }
    }

    private Packet GetNewPacket()
    {
        int totalChance = addedPlants.getTotalChance() * addedPlants.multiplier;
        foreach (PlantList pl in stagePlants)
        {
            totalChance += pl.getTotalChance() * pl.multiplier;
        }
        int n = Random.Range(0, totalChance);
        foreach (PlantList pl in stagePlants)
        {
            n -= pl.totalChance * pl.multiplier;
            if (n < 0) return pl.GetPacket();
        }
        return addedPlants.GetPacket();
    }

    public void Clear()
    {
        for (int i = 0; i < slots; i++)
        {
            Destroy(cMarket[i].gameObject);
        }
        selectPlant(-1);
    }


    public Plant GetPlant()
    {
        if (selectedSlot < 0 || !cMarket[selectedSlot]) return null;
        var plant = cMarket[selectedSlot].plant;
        Destroy(cMarket[selectedSlot].gameObject);
        selectPlant(-1);
        return plant;
    }

    public void selectPlant(int plantNum)
    {
        if (!garden.actionsAllowed) return;
        if (plantNum < 0)
        {
            selector.SetActive(false);
        }
        else
        {
            
            //foreach (var slot in cMarket) if (slot) slot.ShowInfo(false);
            //cMarket[plantNum].ShowInfo(true);
            if (lockers[plantNum].activeSelf) return;
            selector.SetActive(true);
            selector.transform.position = new Vector3(xCoord, yCoords[plantNum]);
        }
        lastSlot = selectedSlot;
        selectedSlot = plantNum;

    }

    public void lockSlot(int slot)
    {
        lockers[slot].SetActive(true);
    }

    public void unlockSlot(int slot)
    {
        lockers[slot].SetActive(false);
    }

    public bool allLocked()
    {
        foreach (var slot in lockers)
            if (!slot.activeSelf) return false;
        return true;
    }

    public void addFuturePacket(Packet p, int turns)
    {
        qPackets.Add(new FuturePacket(turns, p));
    }

    public int getIndexOfList(string listname)
    {
        for (int i = 0; i < stagePlants.Count; i++)
        {
            if (stagePlants[i].name == listname)
            {
                return i;
            }
        }
        return -1;
    }

    public void addPlantList(PlantList p)
    {
        stagePlants.Add(p);
    }

    public void removePlantList(int index)
    {
        stagePlants.RemoveAt(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
