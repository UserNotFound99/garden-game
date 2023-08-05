using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public struct packetProb
{
    public Packet packet;
    public int prob;

    public packetProb(Packet _packet, int _prob)  {
        packet = _packet;
        prob = _prob;
    }
}

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Stage", order = 1)]
public class PlantList : ScriptableObject
{
    [SerializeField] public List<Packet> packets = new List<Packet>();
    [SerializeField] public List<int> probs = new List<int>();
    List<packetProb> probTable = new List<packetProb>();
    [System.NonSerialized] public int totalChance = 0;
    [System.NonSerialized] public bool started = false;

    // Start is called before the first frame update
    public Packet GetPacket()
    {
        if (!started) StageInit();
        int n = Random.Range(0, totalChance);
        for (int i = 0; i < probTable.Count; i++)
        {
            n -= probTable[i].prob;
            if (n < 0) return probTable[i].packet;
        }
        return null;
    }

    public void ResetList()
    {
        probTable = new List<packetProb>();
        totalChance = 0;
        started = false;
    }

    void StageInit()
    {
        if (packets.Count != probs.Count) throw new System.Exception("Packet count for stage " + name + " is incorrect!");
        totalChance = 0;
        for (int i = 0; i < packets.Count; i++)
        {
            probTable.Add(new packetProb(packets[i], probs[i]));
            totalChance += probs[i];
        }
        started = true;
    }

    public int getTotalChance()
    {
        if (!started) StageInit();
        return totalChance;
    }

    public void AddPacket(Packet p, int odds)
    {
        if (!started) StageInit();
        for (int i = 0; i < probTable.Count; i++)
        {
            if (probTable[i].packet.name == p.name)
            {
                totalChance -= probTable[i].prob;
                probTable[i] = new packetProb(p, Mathf.Max(0, probTable[i].prob + odds));
                totalChance += probTable[i].prob;
                return;
            }
        }
        if (odds < 0) throw new System.Exception("Trying to remove packet odds that don't exist!");
        probTable.Add(new packetProb(p, odds));
        totalChance += odds;
    }
}
