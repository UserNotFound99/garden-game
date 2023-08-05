using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [System.NonSerialized] public EventTab eventTab;
    [System.NonSerialized] public Garden garden;

    [SerializeField] public UpgradeGenerator upgradeGen;
    // Start is called before the first frame update
    void Start()
    {
        eventTab = FindObjectOfType<EventTab>();
        garden = GetComponent<Garden>();
    }


    public IEnumerator proceed(int turnNum)
    {
        if (turnNum % 3 == 0)
        {
            garden.actionsAllowed = false;
            eventTab.Up();
            eventTab.Selector(upgradeGen.GetUpgrades(3));
            while (eventTab.result == -1)
            {
                yield return null;
            }
            eventTab.Down();
            yield return new WaitForSeconds(0.5f);
            garden.actionsAllowed = true;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
