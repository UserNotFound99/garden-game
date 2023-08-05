using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventTab : ToggleTab
{
    
    [SerializeField] private List<Vector2> upgradeAnchors;
    

    public void Selector(List<Upgrade> upgrades)
    {
        if (upgrades.Count != upgradeAnchors.Count)
        {
            print("Incorrect arguments to eventTab!");
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < upgrades.Count; i++)
        {
            GameObject temp = Instantiate(upgrades[i].gameObject, (Vector3) upgradeAnchors[i] + transform.position, Quaternion.identity, transform);
            temp.GetComponent<Upgrade>().id = i;
        }
    }
}
