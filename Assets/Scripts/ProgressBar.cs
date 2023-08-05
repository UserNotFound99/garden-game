using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] List<Sprite> stages;
    private SpriteRenderer shown;
    [System.NonSerialized] public SpriteRenderer shownIcon;
    int v = 0;
    
    void Awake()
    {
        shown = GetComponent<SpriteRenderer>();
        shown.sprite = stages[0];


        shownIcon = transform.Find("IconDisplay").GetComponent<SpriteRenderer>();
        shownIcon.sprite = transform.parent.gameObject.GetComponent<Plant>().icon;
        shownIcon.enabled = true;
    }

    public void SetProportion(double d)
    {
        double increment = 1.0 / (stages.Count - 1.0);
        v = (int) ((d + (increment / 2)) / increment);
        shown.sprite = stages[v];
        //if (v == stages.Count - 1) Appear();
    }

    public void SetFraction(int x, int stages)
    {
        SetProportion((double) x /stages);
    }

    public void Disappear()
    {
        //if (!Done())
        //{
            shown.enabled = false;
            shownIcon.enabled = false;
        //}
    }

    public void Appear()
    {
        shown.enabled = true;
        shownIcon.enabled = true;
    }

    public bool Done()
    {
        return (v == stages.Count - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
