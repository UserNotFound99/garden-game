using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> screens;
    int cScreen = -1;
    public void Start()
    {
        foreach (GameObject g in screens)
        {
            g.SetActive(false);
        }
    }

    public void SetScreen(int val)
    {
        if (val == cScreen) val = -1;
        if (cScreen >= 0) screens[cScreen].SetActive(false);
        cScreen = val;
        if (cScreen >= 0) screens[cScreen].SetActive(true);
    }
}
