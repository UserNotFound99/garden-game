using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public struct upgradeProb
{
    public Upgrade upgrade;
    public int prob;

    public upgradeProb(Upgrade _up, int _prob)  {
        upgrade = _up;
        prob = _prob;
    }
}

[CreateAssetMenu(fileName = "UpgradeGen", menuName = "ScriptableObjects/Upgrade Generator", order = 1)]
public class UpgradeGenerator : ScriptableObject
{
    [SerializeField] public List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] public List<int> probs = new List<int>();
    List<upgradeProb> probTable = new List<upgradeProb>();
    int totalChance = 0;
    bool started = false;

    // Start is called before the first frame update
    public List<Upgrade> GetUpgrades(int num)
    {
        if (!started) UpgenInit();
        List<upgradeProb> tempUpgrades = new List<upgradeProb>(probTable);
        int tempChance = totalChance;
        List<Upgrade> selUpgrades = new List<Upgrade>();
        for (int upNum = 0; upNum < num; upNum++)
        {
            int n = Random.Range(0, tempChance);
            for (int i = 0; i < tempUpgrades.Count; i++)
            {
                n -= tempUpgrades[i].prob;
                if (n < 0)
                {
                    selUpgrades.Add(tempUpgrades[i].upgrade);
                    tempChance -= tempUpgrades[i].prob;
                    tempUpgrades.RemoveAt(i);
                    break;
                }
                
            }
        }
        return selUpgrades;
    }

    void UpgenInit()
    {
        if (upgrades.Count != probs.Count) throw new System.Exception();
        totalChance = 0;
        for (int i = 0; i < upgrades.Count; i++)
        {
            probTable.Add(new upgradeProb(upgrades[i], probs[i]));
            totalChance += probs[i];
        }
    }
}
